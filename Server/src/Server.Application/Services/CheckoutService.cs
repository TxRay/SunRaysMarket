using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Core.DomainModels.Payment;
using SunRaysMarket.Shared.Services.Builders;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.Services;

internal class CheckoutService(
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork,
    ICustomerService customerService,
    IOrderService orderService,
    IPaymentService paymentService,
    ITransactionService transactionService
) : ICheckoutService
{
    public async Task<IEnumerable<TimeSlotListModel>> GetCheckoutTimeSlotsAsync(
        int storeId,
        OrderType orderType
    ) => await unitOfWork.TimeSlotRepository.GetAllTimeSlotsAsync(storeId, orderType);

    public Task<TimeSlotModel?> GetCheckoutTimeSlotAsync(int id)
        => unitOfWork.TimeSlotRepository.GetTimeSlotAsync(id);

    public async Task CheckoutAsync(CheckoutSubmitModel model)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user is null)
            return;

        var (orderId, orderAmount) = await orderService.CreateOrderAsync(
            user,
            model.TimeSlotId,
            model.OrderType,
            model.DeliveryAddressId
        );

        if (orderId is null)
            return;

        var customerPaymentId = await customerService.GetCustomerPaymentIdAsync(user);

        if (orderAmount is null || customerPaymentId is null)
            return;

        var chargeInfo = new CreateChargeModel
        {
            Amount = (long)(100 * orderAmount.Value),
            Currency = "usd",
            CustomerPaymentId = customerPaymentId,
            Source = model.PaymentMethodId
        };

        var chargeResponse = await paymentService.CreateCharge(chargeInfo);

        await transactionService.CreateTransactionAsync(
            orderId.Value,
            orderAmount.Value,
            model.BillingAddressId,
            chargeResponse.Id
        );
    }

    public Task CheckoutAsync(
        int timeSlotId,
        OrderType orderType,
        Action<IAddressBuilder> billingAddressBuilder,
        Action<IPaymentMethodBuilder> paymentMethodBuilder,
        Action<IAddressBuilder>? deliveryAddressBuilder
    ) => Task.CompletedTask;

    public async Task CheckoutAsync(ClaimsPrincipal user, CheckoutSubmitModel checkoutSubmitModel)
    {
        var (orderId, orderAmount) = await orderService.CreateOrderAsync(
            user,
            checkoutSubmitModel.TimeSlotId,
            checkoutSubmitModel.OrderType,
            checkoutSubmitModel.DeliveryAddressId
        );

        if (orderId is null)
            return;

        var customerPaymentId = await customerService.GetCustomerPaymentIdAsync(user);

        if (orderId is null || orderAmount is null || customerPaymentId is null)
            return;

        var chargeInfo = new CreateChargeModel
        {
            Amount = (long)(100 * orderAmount.Value),
            Currency = "usd",
            CustomerPaymentId = customerPaymentId,
            Source = checkoutSubmitModel.PaymentMethodId
        };

        var chargeResponse = await paymentService.CreateCharge(chargeInfo);

        await transactionService.CreateTransactionAsync(
            orderId.Value,
            orderAmount.Value,
            checkoutSubmitModel.BillingAddressId,
            chargeResponse.Id
        );
    }
}

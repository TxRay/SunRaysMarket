using System.Linq.Expressions;
using System.Security.Claims;
using Application.Builders;
using Application.DomainModels;
using Application.DomainModels.Checkout;
using Application.DomainModels.Payment;
using Application.Enums;
using Application.UnitOfWork;
using WebClient.Models;

namespace Application.Services;

internal class CheckoutService(
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

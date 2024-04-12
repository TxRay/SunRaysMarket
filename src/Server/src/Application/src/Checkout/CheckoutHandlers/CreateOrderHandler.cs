using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Checkout.Results;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Server.Core.Services;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;

namespace SunRaysMarket.Server.Application.Checkout.CheckoutHandlers;

public class CreateOrderHandler : ICheckoutHandler
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ICustomerService _customerService;
    private readonly ILogger<CreateOrderHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderHandler(
        ICustomerService customerService,
        IHttpContextAccessor contextAccessor,
        ILogger<CreateOrderHandler> logger,
        IUnitOfWork unitOfWork
    )
    {
        _customerService = customerService;
        _contextAccessor = contextAccessor;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<CheckoutHandlerResponse> HandleAsync(CheckoutContext context)
    {
        if (_contextAccessor.HttpContext?.User is null)
            return new CheckoutHandlerResponse.Error("The customer is not currently logged in.");

        var customerId = await _customerService.GetCurrentCustomerIdAsync(
            _contextAccessor.HttpContext.User
        );

        if (customerId is null)
            return new CheckoutHandlerResponse.Error(
                "No customer account was found for the logged in user."
            );

        var newOrder = context.SubmitModel switch
        {
            CheckoutSubmitModel.DeliveryModel deliveryModel
                => new CreateOrderModel
                {
                    CustomerId = customerId.Value,
                    DeliveryAddressId = deliveryModel.DeliveryAddressId,
                    StoreId = deliveryModel.StoreId,
                    OrderType = OrderType.Delivery,
                    TimeSlotId = deliveryModel.TimeSlotId
                },
            CheckoutSubmitModel.PickupModel pickupModel
                => new CreateOrderModel
                {
                    CustomerId = customerId.Value,
                    StoreId = pickupModel.StoreId,
                    OrderType = OrderType.Pickup,
                    TimeSlotId = pickupModel.TimeSlotId
                },
            _ => throw new ArgumentOutOfRangeException()
        };

        try
        {
            await _unitOfWork.OrderRepository.CreateOrderAsync(newOrder);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{}", e.Message);
            return new CheckoutHandlerResponse.Error(
                "Something went wrong while trying to create an order"
            );
        }

        var persistedOrder = _unitOfWork.OrderRepository.GetPersistedOrderDetails();

        return persistedOrder is not null
            ? new CheckoutHandlerResponse.Result<CreateOrderResult>(
                new CreateOrderResult(persistedOrder.Id, persistedOrder.OrderNumber)
            )
            : new CheckoutHandlerResponse.Error("An order could not be created");
    }
}
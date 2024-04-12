using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Checkout.Results;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Server.Core.DomainModels;
using SunRaysMarket.Server.Core.Services;

namespace SunRaysMarket.Server.Application.Checkout.CheckoutHandlers;

public class PopulateOrderHandler : ICheckoutHandler
{
    private readonly ICustomerService _customerService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<PopulateOrderHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PopulateOrderHandler(
        ICustomerService customerService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<PopulateOrderHandler> logger,
        IUnitOfWork unitOfWork
    )
    {
        _customerService = customerService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<CheckoutHandlerResponse> HandleAsync(CheckoutContext context)
    {
        var cartId = await _customerService.GetCustomerCartIdAsync(
            _httpContextAccessor.HttpContext!.User
        );

        if (cartId is null)
            return new CheckoutHandlerResponse.Error(
                "No shopping cart was found for the current user."
            );

        if (!context.HandlerResults.TryGetValue<CreateOrderResult>(out var createOrderResult))
            return new CheckoutHandlerResponse.Error("No order was created.");

        var cartItems = await _unitOfWork.CartRepository.GetCartItemsAsync(cartId.Value);
        var orderLines = cartItems.Select(
            ci =>
                new CreateOrderLineModel
                {
                    OrderId = createOrderResult!.OrderId,
                    ItemId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.RegularPrice,
                    Discount = ci.Discount,
                    TotalPrice = ci.ProductPrice
                }
        );

        try
        {
            await _unitOfWork.OrderRepository.AddOrderLinesAsync(orderLines);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("{}", e.Message);
            return new CheckoutHandlerResponse.Error(
                "Something went wrong while trying populate the order."
            );
        }

        return new CheckoutHandlerResponse.Empty();
    }
}
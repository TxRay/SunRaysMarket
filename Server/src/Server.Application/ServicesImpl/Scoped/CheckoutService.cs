using Microsoft.AspNetCore.Authentication.Cookies;
using SunRaysMarket.Server.Application.Checkout;
using SunRaysMarket.Server.Application.Checkout.CheckoutHandlers;
using SunRaysMarket.Server.Application.Checkout.Results;
using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Core.Checkout;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

internal class CheckoutService(ICustomerService customerService, ICookieService cookieService, IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
    : ICheckoutService
{
    public async Task<IEnumerable<TimeSlotListModel>> GetCheckoutTimeSlotsAsync(
        int storeId,
        OrderType orderType
    ) => await unitOfWork.TimeSlotRepository.GetAllTimeSlotsAsync(storeId, orderType);

    public Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync() =>
        unitOfWork.StoreRepository.GetAllStoresAsync();

    public Task<TimeSlotModel?> GetCheckoutTimeSlotAsync(int id) =>
        unitOfWork.TimeSlotRepository.GetTimeSlotAsync(id);

    public async Task<CheckoutResponse> CheckoutAsync(CheckoutSubmitModel model)
    {
        var checkoutPipeline = ICheckoutPipelineBuilder.Create()
            .AddHandler<CreateOrderHandler>()
            .WithReturnTypeCheck<CreateOrderResult>()
            .AddHandler<PopulateOrderHandler>()
            .AddHandler<UpdateOrderAmountHandler>()
            .WithReturnTypeCheck<UpdateOrderAmountResult>()
            .AddHandler<CreateChargeHandler>()
            .WithReturnTypeCheck<CreateChargeResult>()
            .AddHandler<CreateTransactionHandler>()
            .AddResponseGenerator(GenerateResponse)
            .Build(serviceProvider);

        if (model is not CheckoutSubmitModel.ValidModel validModel)
            return new CheckoutResponse.Failure("An invalid checkout request was sent to the server.");

        var response = await checkoutPipeline.ExecuteAsync(validModel);
        await HandleResponseAsync(response);

        return response;
    }

    private static CheckoutResponse GenerateResponse(CheckoutContext context)
        => new CheckoutResponse.Success(
            OrderNumber: ((CreateOrderResult)context.HandlerResults[typeof(CreateOrderResult)]).OrderNumber
            .ToString(),
            OrderAmount: FormatHelpers.ToCurrencyString(
                ((UpdateOrderAmountResult)context.HandlerResults[typeof(UpdateOrderAmountResult)]).Amount)
        );

    private async Task HandleResponseAsync(CheckoutResponse response)
    {
        switch (response)
        {
            case CheckoutResponse.Failure:
                break;
            case CheckoutResponse.Warning:
                break;
            case CheckoutResponse.Success:
                cookieService.DeleteCookie(cookies => cookies.CartId!);
                await customerService.RemoveCartFromCustomerAsync();
                break;
        }
    }
}
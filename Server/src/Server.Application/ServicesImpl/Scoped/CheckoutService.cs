using SunRaysMarket.Server.Application.Checkout;
using SunRaysMarket.Server.Application.Checkout.CheckoutHandlers;
using SunRaysMarket.Server.Application.Checkout.Results;
using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Core.Checkout;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

internal class CheckoutService : ICheckoutService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutService(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
    {
        _serviceProvider = serviceProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TimeSlotListModel>> GetCheckoutTimeSlotsAsync(
        int storeId,
        OrderType orderType
    ) => await _unitOfWork.TimeSlotRepository.GetAllTimeSlotsAsync(storeId, orderType);

    public Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync() =>
        _unitOfWork.StoreRepository.GetAllStoresAsync();

    public Task<TimeSlotModel?> GetCheckoutTimeSlotAsync(int id) =>
        _unitOfWork.TimeSlotRepository.GetTimeSlotAsync(id);

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
            .Build(_serviceProvider);

        if (model is CheckoutSubmitModel.ValidModel validModel)
        {
            return await checkoutPipeline.ExecuteAsync(validModel);
        }

        return new CheckoutResponse.Failure("An invalid checkout request was sent to the server.");
    }

    private static CheckoutResponse GenerateResponse(CheckoutContext context)
        => new CheckoutResponse.Success(
            OrderNumber: ((CreateOrderResult)context.HandlerResults[typeof(CreateOrderHandler)]).OrderNumber
            .ToString(),
            OrderAmount: FormatHelpers.ToCurrencyString(
                ((UpdateOrderAmountResult)context.HandlerResults[typeof(CreateOrderHandler)]).Amount)
        );
}
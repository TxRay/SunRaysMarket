using SunRaysMarket.Shared.Core.Checkout;
using SunRaysMarket.Shared.Core.DomainModels;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Shared.Core.Services;

public interface ICheckoutService
{
    Task<IEnumerable<TimeSlotListModel>> GetCheckoutTimeSlotsAsync(
        int storeId,
        OrderType orderType
    );

    Task<IEnumerable<StoreListModel>> GetStoreLocationsAsync();
    Task<TimeSlotModel?> GetCheckoutTimeSlotAsync(int id);
    Task<CheckoutResponse> CheckoutAsync(CheckoutSubmitModel model);
}
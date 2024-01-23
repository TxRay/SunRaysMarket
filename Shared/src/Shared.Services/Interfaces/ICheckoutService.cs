using SunRaysMarket.Shared.Core.DomainModels.Checkout;
using SunRaysMarket.Shared.Core.Enums;

namespace SunRaysMarket.Shared.Services.Interfaces;

public interface ICheckoutService
{
    Task<IEnumerable<TimeSlotListModel>> GetCheckoutTimeSlotsAsync(
        int storeId,
        OrderType orderType
    );

    Task CheckoutAsync(CheckoutSubmitModel model);
}

using System.Security.Claims;

namespace SunRaysMarket.Server.Application.Services;

public interface IOrderService
{
    Task<(int?, float?)> CreateOrderAsync(
        ClaimsPrincipal user,
        int timeSlotId,
        OrderType orderType,
        int? deliveryAddressId
    );

    Task CreateInitialOrderLinesAsync(int orderId, ClaimsPrincipal user);
}

using System.Security.Claims;
using SunRaysMarket.Shared.Core.Enums;

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

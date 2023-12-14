using System.Security.Claims;
using Application.DomainModels;
using Application.Enums;
using WebClient.Models;

namespace Application.Services;

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

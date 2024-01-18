using System.Linq.Expressions;
using System.Security.Claims;
using Application.Builders;
using Application.DomainModels;
using Application.DomainModels.Checkout;
using Application.Enums;
using WebClient.Models;

namespace Application.Services;

public interface ICheckoutService
{
    Task<IEnumerable<TimeSlotListModel>> GetCheckoutTimeSlotsAsync(
        int storeId,
        OrderType orderType
    );

    Task CheckoutAsync(CheckoutSubmitModel model);
    
}

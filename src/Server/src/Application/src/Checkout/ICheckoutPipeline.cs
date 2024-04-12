using SunRaysMarket.Shared.Core.Checkout;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;

namespace SunRaysMarket.Server.Application.Checkout;

public interface ICheckoutPipeline
{
    Task<CheckoutResponse> ExecuteAsync(CheckoutSubmitModel.ValidModel submitModel);
}
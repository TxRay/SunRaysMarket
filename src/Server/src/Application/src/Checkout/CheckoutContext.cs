using Microsoft.AspNetCore.Http;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;

namespace SunRaysMarket.Server.Application.Checkout;

public record CheckoutContext(
    HttpContext HttpContext,
    CheckoutSubmitModel.ValidModel SubmitModel,
    IReadOnlyDictionary<Type, object> HandlerResults
);
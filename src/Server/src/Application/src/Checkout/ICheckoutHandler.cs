namespace SunRaysMarket.Server.Application.Checkout;

public interface ICheckoutHandler
{
    Task<CheckoutHandlerResponse> HandleAsync(CheckoutContext context);
}
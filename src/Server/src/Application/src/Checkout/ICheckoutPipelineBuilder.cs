using SunRaysMarket.Shared.Core.Checkout;

namespace SunRaysMarket.Server.Application.Checkout;

public interface ICheckoutPipelineBuilder
{
    IHandlerConfigCheckoutPipelineBuilder AddHandler(Type handlerType);

    IHandlerConfigCheckoutPipelineBuilder AddHandler<THandler>()
        where THandler : ICheckoutHandler;

    public static ICheckoutPipelineBuilder Create()
    {
        return new CheckoutPipelineBuilder();
    }
}

public interface ITerminalCheckoutPipelineBuilder : ICheckoutPipelineBuilder
{
    ITerminalCheckoutPipelineBuilder AddResponseGenerator(
        Func<CheckoutContext, CheckoutResponse> generator
    );

    ICheckoutPipeline Build(IServiceProvider provider);
}

public interface IHandlerConfigCheckoutPipelineBuilder : ITerminalCheckoutPipelineBuilder
{
    IHandlerConfigCheckoutPipelineBuilder WithPreProcessor(
        Func<CheckoutContext, CheckoutContext> preProcessor
    );

    IHandlerConfigCheckoutPipelineBuilder WithPostProcessor(
        Func<CheckoutContext, CheckoutContext> postProcessor
    );

    ITerminalCheckoutPipelineBuilder WithReturnTypeCheck<TReturn>();
}
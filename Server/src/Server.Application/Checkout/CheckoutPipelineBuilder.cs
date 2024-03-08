using Microsoft.Extensions.DependencyInjection;
using SunRaysMarket.Server.Application.Checkout.Exceptions;
using SunRaysMarket.Shared.Core.Checkout;

namespace SunRaysMarket.Server.Application.Checkout;

internal sealed class CheckoutPipelineBuilder : ICheckoutPipelineBuilder
{
    private readonly ICollection<ObjectFactory> _handlerFactories = [];

    public Func<CheckoutContext, CheckoutResponse>? ResponseGenerator { get; set; }

    public IDictionary<Type, Func<CheckoutContext, CheckoutContext>> PreProcessors { get; set; } =
        new Dictionary<Type, Func<CheckoutContext, CheckoutContext>>();

    public IDictionary<Type, Func<CheckoutContext, CheckoutContext>> PostProcessors { get; set; } =
        new Dictionary<Type, Func<CheckoutContext, CheckoutContext>>();

    public IDictionary<
        Type,
        Func<CheckoutContext, bool>
    > HandlerResponseTypeCheckDelegates { get; set; } =
        new Dictionary<Type, Func<CheckoutContext, bool>>();

    public IHandlerConfigCheckoutPipelineBuilder AddHandler(Type handlerType)
    {
        if (!handlerType.IsAssignableTo(typeof(ICheckoutHandler)))
            throw new InvalidOperationException(
                $"The type '{handlerType.FullName}'"
                    + $"is not assignable to '{typeof(ICheckoutHandler).FullName}'"
            );

        _handlerFactories.Add(ActivatorUtilities.CreateFactory(handlerType, []));

        return new HandlerConfigCheckoutPipelineBuilder(this, handlerType);
    }

    public IHandlerConfigCheckoutPipelineBuilder AddHandler<THandler>()
        where THandler : ICheckoutHandler
    {
        return AddHandler(typeof(THandler));
    }

    public ICheckoutPipeline Build(IServiceProvider provider)
    {
        if (_handlerFactories.Count == 0)
            throw new NoPipelineHandlersSetException();

        if (ResponseGenerator is null)
            throw new NoPipelineResponseGeneratorSet();

        var pipelineDelegates = new CheckoutPipeline.PipelineDelegates(
            ResponseGenerator,
            PreProcessors.AsReadOnly(),
            PostProcessors.AsReadOnly(),
            HandlerResponseTypeCheckDelegates
        );

        return ActivatorUtilities.CreateInstance<CheckoutPipeline>(
            provider,
            [_handlerFactories.ToArray(), pipelineDelegates]
        );
    }
}

internal sealed class TerminalCheckoutPipelineBuilder : ITerminalCheckoutPipelineBuilder
{
    private readonly CheckoutPipelineBuilder _mainPipelineBuilder;

    public TerminalCheckoutPipelineBuilder(CheckoutPipelineBuilder mainPipelineBuilder)
    {
        _mainPipelineBuilder = mainPipelineBuilder;
    }

    public IHandlerConfigCheckoutPipelineBuilder AddHandler(Type handlerType)
    {
        return _mainPipelineBuilder.AddHandler(handlerType);
    }

    public IHandlerConfigCheckoutPipelineBuilder AddHandler<THandler>()
        where THandler : ICheckoutHandler
    {
        return _mainPipelineBuilder.AddHandler<THandler>();
    }

    public ITerminalCheckoutPipelineBuilder AddResponseGenerator(
        Func<CheckoutContext, CheckoutResponse> generator
    )
    {
        _mainPipelineBuilder.ResponseGenerator = generator;

        return this;
    }

    public ICheckoutPipeline Build(IServiceProvider provider)
    {
        return _mainPipelineBuilder.Build(provider);
    }
}

internal sealed class HandlerConfigCheckoutPipelineBuilder : IHandlerConfigCheckoutPipelineBuilder
{
    private readonly CheckoutPipelineBuilder _mainPipelineBuilder;
    private readonly Type _handlerType;

    public HandlerConfigCheckoutPipelineBuilder(
        CheckoutPipelineBuilder mainPipelineBuilder,
        Type handlerType
    )
    {
        _mainPipelineBuilder = mainPipelineBuilder;
        _handlerType = handlerType;
    }

    public IHandlerConfigCheckoutPipelineBuilder AddHandler(Type handlerType)
    {
        return _mainPipelineBuilder.AddHandler(handlerType);
    }

    public IHandlerConfigCheckoutPipelineBuilder AddHandler<THandler>()
        where THandler : ICheckoutHandler
    {
        return _mainPipelineBuilder.AddHandler<THandler>();
    }

    public ITerminalCheckoutPipelineBuilder AddResponseGenerator(
        Func<CheckoutContext, CheckoutResponse> generator
    )
    {
        _mainPipelineBuilder.ResponseGenerator = generator;

        return new TerminalCheckoutPipelineBuilder(_mainPipelineBuilder);
    }

    public ICheckoutPipeline Build(IServiceProvider provider)
    {
        return _mainPipelineBuilder.Build(provider);
    }

    public IHandlerConfigCheckoutPipelineBuilder WithPreProcessor(
        Func<CheckoutContext, CheckoutContext> preProcessor
    )
    {
        if (!_mainPipelineBuilder.PreProcessors.TryAdd(_handlerType, preProcessor))
            throw new InvalidOperationException("Only one pre-processor delegate may be set.");

        return this;
    }

    public IHandlerConfigCheckoutPipelineBuilder WithPostProcessor(
        Func<CheckoutContext, CheckoutContext> postProcessor
    )
    {
        if (!_mainPipelineBuilder.PostProcessors.TryAdd(_handlerType, postProcessor))
            throw new InvalidOperationException("Only one post-processor delegate may be set.");

        return this;
    }

    public ITerminalCheckoutPipelineBuilder WithReturnTypeCheck<TReturn>()
    {
        Func<CheckoutContext, bool> postProcessorDelegate = context =>
            context.HandlerResults.ContainsKey(typeof(TReturn));

        if (
            !_mainPipelineBuilder
                .HandlerResponseTypeCheckDelegates
                .TryAdd(_handlerType, postProcessorDelegate)
        )
            throw new InvalidOperationException(
                "Only one return type check per handler can be added."
            );

        return new TerminalCheckoutPipelineBuilder(_mainPipelineBuilder);
    }
}

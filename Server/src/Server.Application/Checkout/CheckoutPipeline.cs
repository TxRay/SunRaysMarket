using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SunRaysMarket.Server.Application.Checkout.Results;
using SunRaysMarket.Shared.Core.Checkout;
using SunRaysMarket.Shared.Core.DomainModels.Checkout;

namespace SunRaysMarket.Server.Application.Checkout;

/*
    public Func<CheckoutContext, CheckoutResponse>? ResponseGenerator { get; set; }

    public IDictionary<Type, Func<CheckoutContext, CheckoutContext>> PreProcessors { get; set; } =
        new Dictionary<Type, Func<CheckoutContext, CheckoutContext>>();

    public IDictionary<Type, Func<CheckoutContext, CheckoutContext>> PostProcessors { get; set; } =
        new Dictionary<Type, Func<CheckoutContext, CheckoutContext>>();

    public IDictionary<Type, Func<CheckoutContext, bool>> HandlerResponseTypeCheckDelegates { get; set; }
        = new Dictionary<Type, Func<CheckoutContext, bool>>();

 */

internal class CheckoutPipeline : ICheckoutPipeline
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CheckoutPipeline> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ObjectFactory[] _handlerFactories;
    private readonly PipelineDelegates _pipelineDelegates;


    [ActivatorUtilitiesConstructor]
    public CheckoutPipeline(
        IHttpContextAccessor httpContextAccessor, ILogger<CheckoutPipeline> logger,
        IServiceProvider serviceProvider, ObjectFactory[] handlerFactories, PipelineDelegates pipelineDelegates)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _handlerFactories = handlerFactories;
        _pipelineDelegates = pipelineDelegates;
    }

    public async Task<CheckoutResponse> ExecuteAsync(CheckoutSubmitModel.ValidModel submitModel)
    {
        if (!_httpContextAccessor.HttpContext?.User.IsAuthenticated() ?? false)
            return new CheckoutResponse.Failure("The user is not logged in.");

        var context =
            new CheckoutContext(_httpContextAccessor.HttpContext!, submitModel, new Dictionary<Type, object>());

        foreach (var factory in _handlerFactories)
        {
            try
            {
                var handlerInstance = (ICheckoutHandler)factory.Invoke(_serviceProvider, []);

                if (_pipelineDelegates.PreProcessors.TryGetValue(handlerInstance.GetType(), out var prepProc))
                {
                    context = prepProc.Invoke(context);
                }

                var response = await handlerInstance.HandleAsync(context);

                switch (response)
                {
                    case CheckoutHandlerResponse.Error errorResponse:
                        return new CheckoutResponse.Failure(errorResponse.ErrorMessage);
                    case CheckoutHandlerResponse.Empty:
                        continue;
                    case CheckoutHandlerResponse.Result result:
                        var newDictionary = context.HandlerResults.ToDictionary();
                        newDictionary.Add(result.ValueType, result.ValueObject);
                        context = context with { HandlerResults = newDictionary };

                        if (_pipelineDelegates.HandlerResponseTypeCheckDelegates.TryGetValue(handlerInstance.GetType(),
                                out var typeCheckDelegate)
                            && !typeCheckDelegate.Invoke(context)
                           )
                            throw new Exception(
                                $"The the pipeline handler '{handlerInstance.GetType().FullName}'" +
                                $"did not return the required type."
                            );

                        if (_pipelineDelegates.PostProcessors.TryGetValue(handlerInstance.GetType(), out var postProc))
                        {
                            context = postProc.Invoke(context);
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("{}", e.Message);
                return new CheckoutResponse.Failure("An unknown error occured during the checkout process.");
            }
        }

        return _pipelineDelegates.ResponseGenerator.Invoke(context);
    }

    public record PipelineDelegates(
        Func<CheckoutContext, CheckoutResponse> ResponseGenerator,
        IReadOnlyDictionary<Type, Func<CheckoutContext, CheckoutContext>> PreProcessors,
        IReadOnlyDictionary<Type, Func<CheckoutContext, CheckoutContext>> PostProcessors,
        IDictionary<Type, Func<CheckoutContext, bool>> HandlerResponseTypeCheckDelegates
    );
}
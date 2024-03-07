namespace SunRaysMarket.Server.Application.Checkout.Exceptions;

public class NoPipelineHandlersSetException(string message = NoPipelineHandlersSetException.DefaultMessage)
    : Exception(message)
{
    private const string DefaultMessage =
        "No handler definitions were provided.  Please set at least one handler, by calling the " +
        "'AddHandler' method.";
}
namespace SunRaysMarket.Server.Application.Checkout.Exceptions;

public class NoPipelineResponseGeneratorSet(
    string message = NoPipelineResponseGeneratorSet.DefaultMessage
) : Exception(message)
{
    private const string DefaultMessage =
        "A response generator delegate must be set using the "
        + "'AddResponseGenerator' method before build can be called.";
}
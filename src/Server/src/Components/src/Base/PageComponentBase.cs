using Microsoft.AspNetCore.Components;

namespace SunRaysMarket.Server.Components.Base;

public abstract class PageComponentBase : OwningComponentBase
{
    [Inject] private PageStateProvider? StateProvider { get; set; }

    protected PageState State
    {
        get
        {
            if (StateProvider is null)
                throw new InvalidOperationException(
                    "The page stat cannot be accessed before the component is initialized."
                );

            return StateProvider.State;
        }
    }
}

public abstract class PageComponentBase<TService> : OwningComponentBase<TService>
    where TService : notnull
{
    [Inject] private PageStateProvider? StateProvider { get; set; }

    protected PageState State
    {
        get
        {
            if (StateProvider is null)
                throw new InvalidOperationException(
                    "The page stat cannot be accessed before the component is initialized."
                );

            return StateProvider.State;
        }
    }
}
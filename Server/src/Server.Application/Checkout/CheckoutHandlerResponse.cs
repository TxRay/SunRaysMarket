namespace SunRaysMarket.Server.Application.Checkout;

public abstract record CheckoutHandlerResponse
{
    public record Error(string? ErrorMessage = default) : CheckoutHandlerResponse;

    public abstract record Ok : CheckoutHandlerResponse;

    public record Empty : Ok;

    public abstract record Result(Type ValueType, object ValueObject) : Ok
    {
        internal object ValueObject { get; init; } = ValueObject;
        public static Result<TValue> Create<TValue>(TValue value) => new(value);
    };

    public record Result<TValue>(TValue Value) : Result(typeof(TValue), Value)
    {
        private TValue Value
        {
            get => (TValue)ValueObject;
            init
            {
                ArgumentNullException.ThrowIfNull(value);
                ValueObject = value;
            }
        }

    };
}
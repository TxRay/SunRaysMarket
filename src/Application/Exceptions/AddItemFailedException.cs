using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class AddItemFailedException : Exception
{
    public AddItemFailedException(string message)
        : base(message) { }

    public AddItemFailedException(string message, Exception inner)
        : base(message, inner) { }
}

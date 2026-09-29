namespace ShopApi.Exceptions;

public class ValidationAppException : Exception
{
    public IEnumerable<string> Errors { get; }

    public ValidationAppException(string message, IEnumerable<string> errors)
        : base(message)
    {
        Errors = errors;
    }
}
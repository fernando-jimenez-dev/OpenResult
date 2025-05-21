namespace OpenResult;

public record Error(
    string Message = "",
    string? Code = null,
    Exception? Exception = null,
    Error? InnerError = null
)
{
    public Error Root
    {
        get
        {
            var current = this;
            while (current.InnerError != null)
                current = current.InnerError;
            return current;
        }
    }

    public bool IsExceptional() => Exception is not null;

    public bool IsExceptional(out Exception? exception)
    {
        exception = Exception;
        return Exception is not null;
    }
}
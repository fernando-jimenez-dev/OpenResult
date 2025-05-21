namespace OpenResult
{
    public record Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error? Error { get; }

        private Result(Error? error = null)
        {
            IsSuccess = error is null;
            Error = error;
        }

        public bool Succeeded() => IsSuccess;

        public bool Failed() => IsFailure;

        public bool Failed(out Error? error)
        {
            error = Error;
            return IsFailure;
        }

        public static Result Success() => new();
        public static Result<T> Success<T>(T value) => Result<T>.Success(value);

        public static Result Failure(Error error) => new(
            error ?? throw new ArgumentNullException(
                nameof(error),
                "Result.Failure was called with a null error. Every failure must provide a non-null Error instance."
            )
        );
    }

    public record Result<TValue>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public TValue? Value { get; }
        public Error? Error { get; }

        private Result(TValue? value, Error? error = null)
        {
            IsSuccess = error is null;
            Error = error;
            Value = value;
        }

        public bool Succeeded() => IsSuccess;

        public bool Succeeded(out TValue? value)
        {
            value = Value;
            return IsSuccess;
        }

        public bool Failed() => IsFailure;

        public bool Failed(out Error? error)
        {
            error = Error;
            return IsFailure;
        }

        public static Result<TValue> Success(TValue value) => new(
            value ?? throw new ArgumentNullException(
                nameof(value),
                "Result<Value>.Success was called with a null value. Every successful result must provide a non-null value."
            )
        );

        public static Result<TValue> Failure(Error error) => new(
            default,
            error ?? throw new ArgumentNullException(
                nameof(error),
                "Result<Value>.Failure was called with a null error. Every failure must provide a non-null Error instance."
            )
        );
    }
}
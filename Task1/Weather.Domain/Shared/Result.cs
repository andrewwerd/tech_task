namespace Weather.Domain.Shared;

public class Result
{
    protected internal Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string Error { get; }

    public static Result Success() => new(true, string.Empty);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, string.Empty);

    public static Result Failure(string error) =>
        new(false, error);

    public static Result<TValue> Failure<TValue>(string error) =>
        new(default, false, error);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(string.Empty);
}

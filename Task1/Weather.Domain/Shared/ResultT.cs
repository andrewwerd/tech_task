namespace Weather.Domain.Shared;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, string error)
        : base(isSuccess, error) =>
        _value = value;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public bool TryGetValue(out TValue value)
    {
        value = default!;
        if (IsSuccess)
        {
            value = _value!;
        }
        return IsSuccess;
    } 

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
    public static implicit operator TValue(Result<TValue> result) => result.Value; 
}
namespace FastFeet.Domain.Commons;

public class Result
{
    protected internal Result(bool isSuccess, IEnumerable<Error> errors)
    {
        if (isSuccess && errors.Any(error => error != Error.None))
            throw new InvalidOperationException();

        if (!isSuccess && errors.Any(error => error == Error.None))
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IEnumerable<Error> Errors { get; }

    public static Result<TValue> Success<TValue>(TValue value)
        => new(value, isSuccess: true, errors: new[] { Error.None });

    public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors)
        => new(default, isSuccess: false, errors);

    public static Result<TValue> Create<TValue>(TValue? value)
        => value is not null ? Success(value) : Failure<TValue>(errors: new[] { Error.NullValue });

}

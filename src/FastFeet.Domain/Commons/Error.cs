namespace FastFeet.Domain.Commons;

public record Error
{
    public Error(string code, string message)
        => (Code, Message) = (code, message);

    public string Code { get; }
    public string Message { get; }

    public static readonly Error None
        = new(string.Empty, string.Empty);
    public static readonly Error NullValue
        = new("Error.NullValue", "The specifed result value is null.");
}
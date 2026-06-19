namespace EventsApp;

public class Result<T>
{
    private Result(IReadOnlyCollection<string> errorMessages)
    {
        if (!errorMessages.Any())
            throw new ArgumentException("No error messages provided");

        IsSuccess = false;
        ErrorMessages = errorMessages;
    }

    private Result(T value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        IsSuccess = true;
        Value = value;
    }

    public T Value { get; }
    public IReadOnlyCollection<string> ErrorMessages { get; }
    public bool IsSuccess { get; init; }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(IReadOnlyCollection<string> errorMessages) => new(errorMessages);
}
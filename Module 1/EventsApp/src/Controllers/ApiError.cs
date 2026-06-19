namespace EventsApp.Controllers;

public record ApiError(int Code, IReadOnlyCollection<string> Messages)
{
    public static ApiError From(int code, string message) =>
        new(code, [message]);

    public static ApiError From(int code, IReadOnlyCollection<string> messages) =>
        new(code, messages);
}

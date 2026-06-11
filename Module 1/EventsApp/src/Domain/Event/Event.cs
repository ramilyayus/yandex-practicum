namespace EventsApp;

public record Event
{
    public required Guid InternalId { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required DateTime StartAt { get; init; }
    public required DateTime EndAt { get; init; }

    private Event()
    {
    }

    public static Result<Event> Create( // todo learn result pattern
        Guid internalId,
        string title,
        string? description,
        DateTime startAt,
        DateTime endAt
    )
    {
        var validationMessages = ValidateEvent(startAt, endAt).ToList();

        if (validationMessages.Any())
            return Result<Event>.Failure(validationMessages);

        return Result<Event>.Success(
            new Event
            {
                InternalId = internalId,
                Title = title,
                Description = description,
                StartAt = startAt,
                EndAt = endAt
            });
    }

    public Result<Event> Update(
        string title,
        string? description,
        DateTime startAt,
        DateTime endAt)
    {
        var validationMessages = ValidateEvent(startAt, endAt).ToList();

        if (validationMessages.Any())
            return Result<Event>.Failure(validationMessages);

        return Result<Event>.Success(
            this with
            {
                Title = title,
                Description = description,
                StartAt = startAt,
                EndAt = endAt
            });
    }

    private static IEnumerable<string> ValidateEvent(
        DateTime startAt,
        DateTime endAt)
    {
        if (startAt > endAt)
            yield return "Start and End time must be before Start";
        yield break;
    }
}
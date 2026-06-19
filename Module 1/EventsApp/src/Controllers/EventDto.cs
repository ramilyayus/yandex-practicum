namespace EventsApp.Controllers;

public record EventDto(
    Guid InternalId,
    string Title,
    string? Description,
    DateTime StartAt,
    DateTime EndAt);
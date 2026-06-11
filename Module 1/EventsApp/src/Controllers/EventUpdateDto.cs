namespace EventsApp.Controllers;

public record EventUpdateDto(
    string Title,
    string? Description,
    DateTime StartAt,
    DateTime EndAt);
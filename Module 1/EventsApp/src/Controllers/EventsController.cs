using Microsoft.AspNetCore.Mvc;

namespace EventsApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(IEventService eventService)
{
    [HttpGet("{internalId}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    public ActionResult<EventDto> GetAsync([FromRoute] Guid internalId)
    {
        var @event = eventService.TryGetByInternal(internalId);
        if (@event is null)
            return new NotFoundObjectResult(ApiError.From(StatusCodes.Status404NotFound, "Event not found"));

        return new OkObjectResult(@event);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public ActionResult<EventDto> CreateAsync([FromBody] EventDto @event)
    {
        var eventToUpdate = eventService.TryGetByInternal(@event.InternalId);
        if (eventToUpdate is not null)
            return new BadRequestObjectResult(ApiError.From(StatusCodes.Status400BadRequest,"Event already exists"));

        var result = Event.Create(
            @event.InternalId,
            @event.Title,
            @event.Description,
            @event.StartAt,
            @event.EndAt);

        if (!result.IsSuccess)
            return new BadRequestObjectResult(ApiError.From(StatusCodes.Status400BadRequest, result.ErrorMessages));

        eventService.Create(result.Value);
        return new OkObjectResult(result.Value);
    }

    [HttpPut("{internalId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public ActionResult UpdateAsync([FromRoute] Guid internalId, [FromBody] EventUpdateDto @event)
    {
        var eventToUpdate = eventService.TryGetByInternal(internalId);
        if (eventToUpdate is null)
            return new NotFoundObjectResult(ApiError.From(StatusCodes.Status404NotFound, "Event not found"));

        var updatedEvent = eventToUpdate.Update(
            title: @event.Title,
            description: @event.Description,
            startAt: @event.StartAt,
            endAt: @event.EndAt);

        if (!updatedEvent.IsSuccess)
            return new BadRequestObjectResult(ApiError.From(StatusCodes.Status400BadRequest, updatedEvent.ErrorMessages));

        eventService.Update(internalId, updatedEvent.Value);
        return new OkResult();
    }

    [HttpDelete("{internalId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult DeleteAsync([FromRoute] Guid internalId)
    {
        var eventToUpdate = eventService.TryGetByInternal(internalId);
        if (eventToUpdate is not null)
            eventService.Delete(internalId);

        return new OkResult();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EventDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<EventDto>> GetAllAsync()
    {
        return new OkObjectResult(eventService.GetAll());
    }
}
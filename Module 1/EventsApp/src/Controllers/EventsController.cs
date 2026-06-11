using Microsoft.AspNetCore.Mvc;

namespace EventsApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(IEventService eventService)
{
    [HttpGet("{internalId}")]
    public ActionResult<EventDto> GetAsync([FromRoute] Guid internalId)
    {
        var @event = eventService.TryGetByInternal(internalId);
        if (@event is null)
            return new NotFoundResult();

        return new OkObjectResult(@event);
    }

    [HttpPost]
    public ActionResult<EventDto> CreateAsync([FromBody] EventDto @event)
    {
        var result = Event.Create(
            @event.InternalId,
            @event.Title,
            @event.Description,
            @event.StartAt,
            @event.EndAt);

        if (result.IsSuccess)
        {
            eventService.Create(result.Value);
            return new OkObjectResult(result.Value);
        }
        else
        {
            return new BadRequestObjectResult(result.ErrorMessages);
        }
    }

    [HttpPut("{internalId}")]
    public ActionResult UpdateAsync([FromRoute] Guid internalId, [FromBody] EventUpdateDto @event)
    {
        var eventToUpdate = eventService.TryGetByInternal(internalId);
        if (eventToUpdate is null)
            return new NotFoundResult();

        var updatedEvent = eventToUpdate.Update(
            title: @event.Title,
            description: @event.Description,
            startAt: @event.StartAt,
            endAt: @event.EndAt);

        if (!updatedEvent.IsSuccess)
            return new BadRequestObjectResult(updatedEvent.ErrorMessages);

        eventService.Update(internalId, updatedEvent.Value);
        return new OkResult();
    }

    [HttpDelete("{internalId}")]
    public ActionResult DeleteAsync([FromRoute] Guid internalId)
    {
        var eventToUpdate = eventService.TryGetByInternal(internalId);
        if (eventToUpdate is not null)
            eventService.Delete(internalId);

        return new OkResult();
    }

    [HttpGet]
    public ActionResult<IEnumerable<EventDto>> GetAllAsync()
    {
        return new OkObjectResult(eventService.GetAll());
    }
}
namespace EventsApp;

public interface IEventService
{
    public Event? TryGetByInternal(Guid internalId);

    public IReadOnlyCollection<Event> GetAll();

    void Create(Event @event);

    void Update(Guid internalId, Event @event);

    public void Delete(Guid internalId);
}

public class EventService(IEventRepository eventRepository) : IEventService
{
    public Event? TryGetByInternal(Guid internalId) => eventRepository.TryGetByInternal(internalId);

    public IReadOnlyCollection<Event> GetAll() => eventRepository.GetAll();

    public void Create(Event @event) => eventRepository.Create(@event);

    public void Update(Guid internalId, Event @event) => eventRepository.Update(internalId, @event);

    public void Delete(Guid internalId) => eventRepository.Delete(internalId);
}
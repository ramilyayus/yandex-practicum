using EventsApp;

public interface IEventRepository
{
    IReadOnlyCollection<Event> GetAll();
    Event? TryGetByInternal(Guid internalId);

    void Create(Event @event);

    void Update(Guid internalId, Event @event);

    void Delete(Guid internalId);
}

public class EventRepository : IEventRepository
{
    private readonly Dictionary<int, Event> _events =
        new()
        {
            {
                1, Event.Create(
                    internalId: Guid.NewGuid(),
                    title: "New Year",
                    description: null,
                    startAt: new DateTime(2025, 12, 31),
                    endAt: new DateTime(2026, 01, 01)).Value
            },

            {
                2, Event.Create(
                    internalId: Guid.NewGuid(),
                    title: "My Birthday",
                    description: null,
                    startAt: new DateTime(2026, 05, 30),
                    endAt: new DateTime(2026, 05, 31)).Value
            }
        };

    public IReadOnlyCollection<Event> GetAll()
    {
        return _events.Values;
    }

    public Event? TryGetByInternal(Guid internalId)
    {
        return _events.SingleOrDefault(x => x.Value.InternalId == internalId).Value;
    }

    public void Create(Event @event)
    {
        if (_events.Any(x => x.Value.InternalId == @event.InternalId))
        {
            throw new InvalidOperationException("Event already exists");
        }
        
        _events.Add(_events.Keys.Max() + 1, @event);
    }

    public void Update(Guid internalId, Event @event)
    {
        var existingEvent = _events.Single(x => x.Value.InternalId == internalId);
        if (existingEvent.Equals(new KeyValuePair<int, Event>(0, null)))
        {
            throw new InvalidOperationException("Event not found");
        }
        
        _events[existingEvent.Key] = @event;
    }

    public void Delete(Guid internalId)
    {
        var existingEvent = _events.SingleOrDefault(x => x.Value.InternalId == internalId);
        if (existingEvent.Equals(new KeyValuePair<int, Event>(0, null)))
        {
            throw new KeyNotFoundException();
        }

        _events.Remove(existingEvent.Key);
    }
}
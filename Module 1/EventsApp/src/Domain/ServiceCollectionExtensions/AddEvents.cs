using EventsApp;

public static class AddEventsExtensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<IEventService, EventService>();
        return services;
    }
}
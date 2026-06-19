using EventsApp;

public static class AddModelExtensions
{
    public static IServiceCollection AddModel(this IServiceCollection services)
    {
        services.AddSingleton<IEventService, EventService>();
        return services;
    }
}
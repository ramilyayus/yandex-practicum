namespace EventsApp;

public static class AddRepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IEventRepository, EventRepository>();
        return services;
    }
}
namespace EventsApp.Infrastructure.Exceptions;

public static class AddExceptionsExtensions
{
    public static IServiceCollection AddExceptions(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }
}
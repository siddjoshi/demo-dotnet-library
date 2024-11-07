using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeatherServices(this IServiceCollection services, string apiKey)
    {
        services.AddSingleton<IWeatherService>(new WeatherService(apiKey));
        return services;
    }
}
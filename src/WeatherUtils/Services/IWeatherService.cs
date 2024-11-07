public interface IWeatherService
{
    Task<WeatherData> GetCurrentWeatherAsync(double latitude, double longitude);
    Task<List<ForecastData>> GetForecastAsync(double latitude, double longitude, int days = 5);
    Task<List<LocationData>> SearchLocationAsync(string query);
}
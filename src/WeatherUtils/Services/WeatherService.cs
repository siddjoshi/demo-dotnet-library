using System.Text.Json;
using System.Net.Http.Json;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5";
    private const string GeoUrl = "https://api.openweathermap.org/geo/1.0";

    public WeatherService(string apiKey)
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
    }

    public async Task<WeatherData> GetCurrentWeatherAsync(double latitude, double longitude)
    {
        var url = $"{BaseUrl}/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";
        var response = await _httpClient.GetFromJsonAsync<JsonElement>(url);

        return new WeatherData
        {
            Temperature = response.GetProperty("main").GetProperty("temp").GetDouble(),
            FeelsLike = response.GetProperty("main").GetProperty("feels_like").GetDouble(),
            Humidity = response.GetProperty("main").GetProperty("humidity").GetInt32(),
            Description = response.GetProperty("weather")[0].GetProperty("description").GetString(),
            Icon = response.GetProperty("weather")[0].GetProperty("icon").GetString(),
            WindSpeed = response.GetProperty("wind").GetProperty("speed").GetDouble(),
            WindDegree = response.GetProperty("wind").GetProperty("deg").GetInt32(),
            Pressure = response.GetProperty("main").GetProperty("pressure").GetDouble(),
            Sunrise = DateTimeOffset.FromUnixTimeSeconds(response.GetProperty("sys").GetProperty("sunrise").GetInt64()).DateTime,
            Sunset = DateTimeOffset.FromUnixTimeSeconds(response.GetProperty("sys").GetProperty("sunset").GetInt64()).DateTime,
            LocationName = response.GetProperty("name").GetString(),
            Country = response.GetProperty("sys").GetProperty("country").GetString()
        };
    }

    public async Task<List<ForecastData>> GetForecastAsync(double latitude, double longitude, int days = 5)
    {
        var url = $"{BaseUrl}/forecast?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";
        var response = await _httpClient.GetFromJsonAsync<JsonElement>(url);
        var forecast = new List<ForecastData>();

        foreach (var item in response.GetProperty("list").EnumerateArray().Take(days * 8))
        {
            forecast.Add(new ForecastData
            {
                Date = DateTimeOffset.FromUnixTimeSeconds(item.GetProperty("dt").GetInt64()).DateTime,
                MinTemperature = item.GetProperty("main").GetProperty("temp_min").GetDouble(),
                MaxTemperature = item.GetProperty("main").GetProperty("temp_max").GetDouble(),
                Description = item.GetProperty("weather")[0].GetProperty("description").GetString(),
                Icon = item.GetProperty("weather")[0].GetProperty("icon").GetString()
            });
        }

        return forecast;
    }

    public async Task<List<LocationData>> SearchLocationAsync(string query)
    {
        var url = $"{GeoUrl}/direct?q={Uri.EscapeDataString(query)}&limit=5&appid={_apiKey}";
        var response = await _httpClient.GetFromJsonAsync<JsonElement[]>(url);

        return response.Select(location => new LocationData
        {
            Name = location.GetProperty("name").GetString(),
            Country = location.GetProperty("country").GetString(),
            Latitude = location.GetProperty("lat").GetDouble(),
            Longitude = location.GetProperty("lon").GetDouble(),
            State = location.TryGetProperty("state", out var state) ? state.GetString() : null
        }).ToList();
    }
}
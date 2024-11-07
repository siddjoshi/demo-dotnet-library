public class WeatherData
{
    public required string Description { get; set; }
    public required string Icon { get; set; }
    public double Temperature { get; set; }
    public double FeelsLike { get; set; }
    public int Humidity { get; set; }
    public double WindSpeed { get; set; }
    public int WindDegree { get; set; }
    public double Pressure { get; set; }
    public required string LocationName { get; set; }
    public required string Country { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
}

public class ForecastData
{
    public DateTime Date { get; set; }
    public required string Description { get; set; }
    public required string Icon { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
}

public class LocationData
{
    public required string Name { get; set; }
    public required string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? State { get; set; }  // Made nullable since it's optional
}

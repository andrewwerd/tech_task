namespace Weather.Application.Options;
public class WeatherApiOptions
{
    public string ApiKey { get; set; } = null!;
    public string City { get; set; } = null!;
}
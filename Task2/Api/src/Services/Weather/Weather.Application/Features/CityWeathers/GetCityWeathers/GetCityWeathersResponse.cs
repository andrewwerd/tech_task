namespace Weather.Application.Features.CityWeathers.GetCityWeathers;
public class GetCityWeathersResponse(List<GetCityWeathersResponse.CityWeather> data)
{
    public List<CityWeather> Data { get; set; } = data;
    public class CityWeather
    {
        public DateTimeOffset Timestamp { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
    }
}
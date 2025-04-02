namespace Weather.Domain.Models;
public class CityWeather: BaseEntity<Guid>
{
    public Guid CityId { get; set; }
    public City City { get; set; } = null!;
    public DateTimeOffset Timestamp { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
}
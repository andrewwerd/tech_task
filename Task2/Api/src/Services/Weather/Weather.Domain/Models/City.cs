namespace Weather.Domain.Models;
public class City : BaseEntity<Guid>
{
    public Guid CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<CityWeather> CityWeathers { get; set; } = [];
}

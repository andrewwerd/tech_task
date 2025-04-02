namespace Weather.Domain.Models;
public class Country : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public List<City> Cities { get; set; } = [];
}
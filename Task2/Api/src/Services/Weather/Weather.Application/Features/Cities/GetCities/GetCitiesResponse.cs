namespace Weather.Application.Features.Cities.GetCities;
public class GetCitiesResponse(List<GetCitiesResponse.City> data)
{
    public List<City> Data { get; set; } = data;
    public class City
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
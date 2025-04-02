namespace Weather.Application.Features.Countries.GetCountries;
public class GetCountriesResponse(List<GetCountriesResponse.Country> data)
{
    public List<Country> Data { get; set; } = data;
    public class Country
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
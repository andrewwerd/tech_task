using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using System.Web;
using Weather.Application.Abstraction;
using Weather.Application.Models;
using Weather.Application.Options;

namespace Weather.Application.Services;
internal class WeatherApiService(HttpClient httpClient, IOptions<WeatherApiOptions> options) : IWeatherApiService
{
    public async Task<WeatherPayload> GetWeatherForecastAsync(double lon, double lat, CancellationToken cancellationToken)
    {
        var uriBuilder = new UriBuilder(httpClient.BaseAddress!.ToString());
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query[nameof(lon)] = lon.ToString(CultureInfo.CurrentCulture);
        query[nameof(lat)] = lat.ToString(CultureInfo.CurrentCulture);
        query["appid"] = options.Value.ApiKey;

        uriBuilder.Query = query.ToString();
        var requestUrl = uriBuilder.ToString();

        var response = await httpClient.GetAsync(requestUrl, cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var weatherPayload = JsonConvert.DeserializeObject<WeatherPayload>(content)!;

        return weatherPayload;
    }
}

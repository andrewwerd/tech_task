using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Web;
using Weather.Application.Abstraction;
using Weather.Application.Options;
using Weather.Domain.Entities;
using Weather.Domain.Shared;

namespace Weather.Application.Services;
public class WeatherService(HttpClient httpClient, IOptions<WeatherApiOptions> options) : IWeatherService
{
    public async Task<Result<WeatherPayload>> GetWeatherAsync(CancellationToken cancellationToken = default)
    {
        var uriBuilder = new UriBuilder(httpClient.BaseAddress!.ToString());
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["q"] = options.Value.City;
        query["appid"] = options.Value.ApiKey;

        uriBuilder.Query = query.ToString();
        var requestUrl = uriBuilder.ToString();

        try
        {
            var response = await httpClient.GetAsync(requestUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            var weatherPayload = JsonConvert.DeserializeObject<WeatherPayload>(content);

            if (weatherPayload is null)
            {
                return Result.Failure<WeatherPayload>("Failed to deserialize weather payload.");
            }

            return weatherPayload;
        }
        catch (JsonReaderException e)
        {
            return Result.Failure<WeatherPayload>("Failed to deserialize weather payload.");
        }
        catch (HttpRequestException e)
        {
            return Result.Failure<WeatherPayload>(e.Message);
        }
        catch (Exception e)
        {
            return Result.Failure<WeatherPayload>(e.Message);
        }
    }
}
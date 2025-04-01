using Microsoft.Azure.Functions.Worker;
using Weather.Application.Abstraction;

namespace Weather.Api.Functions;
public class FetchWeather(
    IWeatherService weatherService,
    ILogStorageService logStorageService,
    IBlobStorageService blobStorageService)
{
    [Function(nameof(FetchWeather))]
    public async Task Run([TimerTrigger("0 * * * * *")] TimerInfo timerInfo, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var timestamp = timerInfo.ScheduleStatus?.Last ?? DateTime.Now;
        var weather = await weatherService.GetWeatherAsync(cancellationToken);

        if (weather.IsFailure)
        {
            await logStorageService
                .SaveFailureLog(id, timestamp, weather.Error, cancellationToken);
            return;
        }

        await logStorageService
            .SaveSuccessLog(id, timestamp, cancellationToken);

        await blobStorageService.SaveWeatherPayload(id, weather.Value, cancellationToken);
    }
}

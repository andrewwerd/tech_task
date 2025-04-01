using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Weather.Application.Abstraction;

namespace Weather.Api.Functions;
public class GetWeather(IBlobStorageService blobStorageService)
{

    [Function(nameof(GetWeather))]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
        CancellationToken cancellationToken)
    {
        var logIdString = req.Query["logId"];

        if (!Guid.TryParse(logIdString, out var logId))
            return new BadRequestObjectResult("Invalid log id format");

        var result = await blobStorageService.GetWeatherPayload(logId, cancellationToken);

        return new OkObjectResult(result);
    }
}

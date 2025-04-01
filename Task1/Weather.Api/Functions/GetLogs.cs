using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Weather.Application.Abstraction;
using Weather.Domain.Entities;

namespace Weather.Api.Functions;
public class GetLogs(ILogStorageService logStorageService)
{
    [Function(nameof(GetLogs))]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
        CancellationToken cancellationToken)
    {
        var periodStartString = req.Query["start"];
        var periodEndString = req.Query["end"];

        if (!DateTimeOffset.TryParse(periodStartString, out var periodStart) ||
           !DateTimeOffset.TryParse(periodEndString, out var periodEnd))
            return new BadRequestObjectResult("Invalid date format");

        var result = new List<WeatherLog>();
        await foreach (var log in
                       logStorageService.GetWeatherLogs(periodStart, periodEnd, cancellationToken))
        {
            result.Add(log);
        }

        result = result
           .OrderByDescending(log => log.Timestamp)
           .ToList();

        return new OkObjectResult(result);
    }
}
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Weather.Application.Abstraction;
using Weather.Application.Options;
using Weather.Domain.Entities;

namespace Weather.Application.Services;
public class BlobStorageService(BlobServiceClient blobServiceClient, IOptions<BlobOptions> options) : IBlobStorageService
{
    public async Task SaveWeatherPayload(Guid id, WeatherPayload entity, CancellationToken cancellationToken = default)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(options.Value.ContainerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        var blobClient = containerClient.GetBlobClient(id.ToString());

        var content = JsonConvert.SerializeObject(entity);

        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        await blobClient.UploadAsync(stream, overwrite: true, cancellationToken: cancellationToken);
    }

    public async Task<WeatherPayload?> GetWeatherPayload(Guid id, CancellationToken cancellationToken = default)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(options.Value.ContainerName);
        var blobClient = containerClient.GetBlobClient(id.ToString());

        if (!await blobClient.ExistsAsync(cancellationToken))
            return null;

        var response = await blobClient.DownloadAsync(cancellationToken);
        using var reader = new StreamReader(response.Value.Content);

        var json = await reader.ReadToEndAsync(cancellationToken);

        return JsonConvert.DeserializeObject<WeatherPayload>(json);
    }
}
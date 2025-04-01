using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System.Text;
using Weather.Application.Options;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Tests.Services;
public class BlobStorageServiceTests
{
    private readonly Mock<BlobServiceClient> _blobServiceClientMock;
    private readonly Mock<BlobClient> _blobClientMock;
    private readonly BlobStorageService _blobStorageService;

    public BlobStorageServiceTests()
    {
        _blobServiceClientMock = new Mock<BlobServiceClient>();
        _blobClientMock = new Mock<BlobClient>();

        var optionsMock = new Mock<IOptions<BlobOptions>>();
        var blobContainerClientMock = new Mock<BlobContainerClient>();

        optionsMock.Setup(o => o.Value).Returns(new BlobOptions { ContainerName = "test-container" });

        _blobServiceClientMock
            .Setup(b => b.GetBlobContainerClient(It.IsAny<string>()))
            .Returns(blobContainerClientMock.Object);

        blobContainerClientMock
            .Setup(b => b.GetBlobClient(It.IsAny<string>()))
            .Returns(_blobClientMock.Object);

        _blobStorageService = new BlobStorageService(_blobServiceClientMock.Object, optionsMock.Object);
    }

    [Fact]
    public async Task SaveWeatherPayload_ShouldUploadBlob()
    {
        // Arrange
        var id = Guid.NewGuid();
        var weatherPayload = new WeatherPayload { ApiId = 1, Name = "Test" };
        var containerClientMock = new Mock<BlobContainerClient>();
        var blobClientMock = new Mock<BlobClient>();

        _blobServiceClientMock
            .Setup(b => b.GetBlobContainerClient(It.IsAny<string>()))
            .Returns(containerClientMock.Object);

        containerClientMock
            .Setup(c => c.GetBlobClient(id.ToString()))
            .Returns(blobClientMock.Object);

        // Act
        await _blobStorageService.SaveWeatherPayload(id, weatherPayload, CancellationToken.None);

        // Assert
        containerClientMock.Verify(c => c.CreateIfNotExistsAsync(It.IsAny<PublicAccessType>(), null, null, CancellationToken.None), Times.Once);
        blobClientMock.Verify(b => b.UploadAsync(It.IsAny<Stream>(), true, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetWeatherPayload_ShouldReturnPayload_WhenBlobExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var weatherPayload = new WeatherPayload();
        var content = JsonConvert.SerializeObject(weatherPayload);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var response = BlobsModelFactory.BlobDownloadInfo(content: stream);

        _blobClientMock
            .Setup(b => b.ExistsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(true, Mock.Of<Response>()));

        _blobClientMock
            .Setup(b => b.DownloadAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(response, Mock.Of<Response>()));

        // Act
        var result = await _blobStorageService.GetWeatherPayload(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(weatherPayload.ApiId, result.ApiId);
    }

    [Fact]
    public async Task GetWeatherPayload_ShouldReturnNull_WhenBlobDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();

        _blobClientMock
            .Setup(b => b.ExistsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(false, Mock.Of<Response>()));

        // Act
        var result = await _blobStorageService.GetWeatherPayload(id);

        // Assert
        Assert.Null(result);
    }
}
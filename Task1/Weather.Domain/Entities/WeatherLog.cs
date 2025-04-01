using Azure;
using Azure.Data.Tables;

namespace Weather.Domain.Entities;
public class WeatherLog : ITableEntity
{
    public string PartitionKey { get; set; } = null!;
    public string RowKey { get; set; } = null!;
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public string Content { get; set; } = null!;
}

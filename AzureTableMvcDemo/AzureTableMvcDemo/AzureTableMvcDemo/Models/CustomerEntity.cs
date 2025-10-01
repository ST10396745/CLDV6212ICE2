using Azure;
using Azure.Data.Tables;

namespace AzureTableMvcDemo.Models;

public class CustomerEntity : ITableEntity
{
    public string PartitionKey { get; set; } = "Customers";
    public string RowKey { get; set; } = Guid.NewGuid().ToString();

    public string? Name { get; set; }
    public string? Email { get; set; }

    public ETag ETag { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}

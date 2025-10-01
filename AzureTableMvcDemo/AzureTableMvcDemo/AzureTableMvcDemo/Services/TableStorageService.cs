using Azure.Data.Tables;
using AzureTableMvcDemo.Models;
using Microsoft.Extensions.Options;

namespace AzureTableMvcDemo.Services;

public class AzureStorageOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TableName { get; set; } = "Customers";
}

public interface ITableStorageService
{
    Task<List<CustomerEntity>> GetCustomersAsync(CancellationToken ct = default);
    Task AddCustomerAsync(CustomerEntity entity, CancellationToken ct = default);
}

public class TableStorageService : ITableStorageService
{
    private readonly TableClient _table;

    public TableStorageService(IOptions<AzureStorageOptions> options)
    {
        var cfg = options.Value;
        var serviceClient = new TableServiceClient(cfg.ConnectionString);
        _table = serviceClient.GetTableClient(cfg.TableName);
        _table.CreateIfNotExists();
    }

    public async Task<List<CustomerEntity>> GetCustomersAsync(CancellationToken ct = default)
    {
        var results = new List<CustomerEntity>();
        await foreach (var entity in _table.QueryAsync<CustomerEntity>(c => c.PartitionKey == "Customers", cancellationToken: ct))
        {
            results.Add(entity);
        }
        return results.OrderByDescending(c => c.Timestamp).ToList();
    }

    public async Task AddCustomerAsync(CustomerEntity entity, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(entity.PartitionKey)) entity.PartitionKey = "Customers";
        if (string.IsNullOrWhiteSpace(entity.RowKey)) entity.RowKey = Guid.NewGuid().ToString();
        await _table.AddEntityAsync(entity, ct);
    }
}

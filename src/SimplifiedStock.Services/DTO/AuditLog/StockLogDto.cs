using SimplifiedStock.Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace SimplifiedStock.Services.DTO.AuditLog;

public record StockLogDto
{
    [JsonPropertyName("type")]
    public OperationType Type { get; init; }

    [JsonPropertyName("wallet_id")]
    public Guid WalletId { get; init; }

    [JsonPropertyName("stock_name")]
    public string StockName { get; init; } = null!;
}

using System.Text.Json.Serialization;
namespace SimplifiedStock.Services.DTO.AuditLog;

public record LogResponse()
{
    [JsonPropertyName("log")]
    public IReadOnlyCollection<StockLogDto> Log { get; set; } = [];
}

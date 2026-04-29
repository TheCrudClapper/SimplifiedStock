using System.Text.Json.Serialization;

namespace SimplifiedStock.Services.DTO.Stock;

public record BankStockAddRequest
{
    [JsonPropertyName("stocks")]
    public IEnumerable<StockDto> Stocks { get; set; } = [];
}

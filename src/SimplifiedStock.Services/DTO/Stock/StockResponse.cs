using System.Text.Json.Serialization;

namespace SimplifiedStock.Services.DTO.Stock;

public record StockResponse() 
{
    [JsonPropertyName("stocks")]
    public IReadOnlyCollection<StockDto> Stocks { get; set; } = [];
};

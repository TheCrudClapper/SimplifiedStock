using SimplifiedStock.Services.DTO.Stock;
using System.Text.Json.Serialization;

namespace SimplifiedStock.Services.DTO.Wallet;

public record WalletResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("stocks")]
    public IReadOnlyCollection<StockDto> Stocks { get; set; } = [];
}
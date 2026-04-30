using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimplifiedStock.Services.DTO.Stock;

public record StockDto()
{
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; init; } = null!;

    [JsonPropertyName("quantity")]
    [Required]
    public int Quantity { get; init; }
};

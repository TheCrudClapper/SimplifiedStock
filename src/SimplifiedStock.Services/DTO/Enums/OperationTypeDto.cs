using System.Text.Json.Serialization;

namespace SimplifiedStock.Services.DTO.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OperationTypeDto
{
    [JsonPropertyName("buy")]
    Buy = 0,

    [JsonPropertyName("sell")]
    Sell = 1
}

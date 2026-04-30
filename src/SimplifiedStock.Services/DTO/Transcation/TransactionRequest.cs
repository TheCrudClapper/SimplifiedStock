using SimplifiedStock.Services.DTO.Enums;
using System.Text.Json.Serialization;

namespace SimplifiedStock.Services.DTO.Transcation;

public record TransactionRequest
{
    [JsonPropertyName("type")]
    public OperationTypeDto Type { get; init; }
}

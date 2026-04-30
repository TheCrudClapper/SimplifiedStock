using SimplifiedStock.Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace SimplifiedStock.Services.DTO.Transcation;

public record TransactionRequest
{
    [JsonPropertyName("type")]
    public OperationType Type { get; init; }
}

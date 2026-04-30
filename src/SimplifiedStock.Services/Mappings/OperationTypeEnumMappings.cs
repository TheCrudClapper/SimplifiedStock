using SimplifiedStock.Domain.Entities.Enums;
using SimplifiedStock.Services.DTO.Enums;

namespace SimplifiedStock.Services.Mappings;

public static class OperationTypeEnumMappings
{
    public static OperationType ToDomain(this OperationTypeDto dto)
    {
        return dto switch
        {
            OperationTypeDto.Buy => OperationType.Buy,
            OperationTypeDto.Sell => OperationType.Sell,
            _ => throw new ArgumentOutOfRangeException(nameof(dto)),
        };
    }

    public static OperationTypeDto ToDto(this OperationType domain)
    {
        return domain switch
        {
            OperationType.Buy => OperationTypeDto.Buy,
            OperationType.Sell => OperationTypeDto.Sell,
            _ => throw new ArgumentOutOfRangeException(nameof(domain)),
        };
    }
}

namespace SimplifiedStock.Services.DTO.AuditLog;

public record LogResponse(IReadOnlyCollection<StockLogDto> log);

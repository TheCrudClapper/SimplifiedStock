namespace SimplifiedStock.Services.DTO.Stock;

public record StockResponse(IReadOnlyCollection<StockDto> stocks);

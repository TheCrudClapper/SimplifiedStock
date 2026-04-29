
using Microsoft.AspNetCore.Mvc;
using SimplifiedStock.Services.DTO.Stock;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.API.Controllers;

[Route("stocks")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IStockService _stockService;
    public StocksController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpPost]
    public async Task<ActionResult> PostStocks(IEnumerable<StockAddRequest> request)
    {
        await _stockService.PostBankStocksAsync(request);
        return Ok();
    }
}

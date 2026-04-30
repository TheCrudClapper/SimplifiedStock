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
    public async Task<ActionResult> PostStocks([FromBody] BankStockAddRequest request)
    {
        await _stockService.AddBankStocksAsync(request);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<StockResponse>> GetBankStocks(CancellationToken ct)
    {
        return Ok(await _stockService.GetAllBankStocksAsync(ct));
    }
}

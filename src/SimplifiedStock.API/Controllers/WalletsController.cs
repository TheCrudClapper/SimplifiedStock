using Microsoft.AspNetCore.Mvc;
using SimplifiedStock.Services.DTO.Transcation;
using SimplifiedStock.Services.DTO.Wallet;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.API.Controllers;

[Route("wallets")]
[ApiController]
public class WalletsController : ControllerBase
{
    private readonly IWalletService _walletService;
    private readonly ITransactionService _transactionService;

    public WalletsController(IWalletService walletService, ITransactionService transactionService)
    {
        _walletService = walletService;
        _transactionService = transactionService;
    }

    [HttpGet("{wallet_id:guid}")]
    public async Task<ActionResult<WalletResponse?>> GetWalletById(Guid wallet_id, CancellationToken ct)
    {
        var wallet = await _walletService.GetWalletByIdAsync(wallet_id, ct);
        if (wallet is null)
            return NotFound();

        return Ok(wallet);
    }

    [HttpPost("{wallet_id:guid}/stocks/{stock_name}")]
    public async Task<ActionResult> ExecuteStockTransaction(Guid wallet_id, string stock_name, [FromBody] TransactionRequest request)
    {
        await _transactionService.BuyOrSellStock(wallet_id, stock_name, request);
        return Ok();
    }

    [HttpGet("{wallet_id:guid}/stocks/{stock_name}")]
    public async Task<ActionResult<int>> GetWalletStockQuantity(Guid wallet_id, string stock_name, CancellationToken ct)
        => await _walletService.GetWalletStockQuantityAsync(wallet_id, stock_name, ct);
}

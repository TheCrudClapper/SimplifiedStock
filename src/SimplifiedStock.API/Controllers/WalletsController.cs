using Microsoft.AspNetCore.Mvc;
using SimplifiedStock.Services.DTO.Wallet;
using SimplifiedStock.Services.ServiceContracts;

namespace SimplifiedStock.API.Controllers;

[Route("wallets")]
[ApiController]
public class WalletsController : ControllerBase
{
    private readonly IWalletService _walletService;
    public WalletsController(IWalletService walletService)
        => _walletService = walletService;

    [HttpGet("{wallet_id:guid}")]
    public async Task<ActionResult<WalletResponse?>> GetWalletById(Guid wallet_id, CancellationToken ct)
    {
        var wallet = await _walletService.GetWalletByIdAsync(wallet_id, ct);
        if (wallet is null)
            return NotFound();

        return Ok(wallet);
    }

    [HttpGet("{wallet_id:guid}/stocks/{stock_name:alpha}")]
    public async Task<ActionResult<int>> GetWalletStockQuantity(Guid wallet_id, string stock_name, CancellationToken ct)
        => await _walletService.GetWalletStockQuantityAsync(wallet_id, stock_name, ct);
}

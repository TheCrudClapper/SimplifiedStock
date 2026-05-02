using FluentAssertions;
using SimplifiedStock.IntegrationTests.Base;
using SimplifiedStock.Services.DTO.Wallet;
using System.Net.Http.Json;

namespace SimplifiedStock.IntegrationTests;

[Collection("Integration")]
public class WalletFlowTests : IntegrationTestBase
{
    public WalletFlowTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Get_Wallet_Stock_Should_Return_Zero_When_Not_Owned()
    {
        // Arrange
        await ResetDb();
        await SeedStock();

        var walletId = Guid.NewGuid();

        // Act
        var quantity = await Client.GetFromJsonAsync<int>(
            $"/wallets/{walletId}/stocks/AAPL");

        // Assert
        quantity.Should().Be(0);
    }

    [Fact]
    public async Task Get_Wallet_Stock_Should_Return_Correct_Quantity()
    {
        // Arrange
        await ResetDb();
        await SeedStock();

        var walletId = Guid.NewGuid();

        await Client.PostAsJsonAsync($"/wallets/{walletId}/stocks/AAPL", new { type = "buy" });

        // Act
        var quantity = await Client.GetFromJsonAsync<int>(
            $"/wallets/{walletId}/stocks/AAPL");

        // Assert
        quantity.Should().Be(1);
    }

    [Fact]
    public async Task Get_Wallet_Should_Return_Correct_State()
    {
        // Arrange
        await ResetDb();
        await SeedStock();

        var walletId = Guid.NewGuid();

        await Client.PostAsJsonAsync($"/wallets/{walletId}/stocks/AAPL", new { type = "buy" });
        await Client.PostAsJsonAsync($"/wallets/{walletId}/stocks/AAPL", new { type = "buy" });

        // Act
        var wallet = await Client.GetFromJsonAsync<WalletResponse>(
            $"/wallets/{walletId}");

        // Assert
        wallet.Should().NotBeNull();
        wallet!.Stocks.Should().ContainSingle(x => x.Name == "AAPL" && x.Quantity == 2);
    }
}

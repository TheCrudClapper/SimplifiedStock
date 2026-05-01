using FluentAssertions;
using SimplifiedStock.IntegrationTests.Base;
using SimplifiedStock.Services.DTO.Wallet;
using System.Net;
using System.Net.Http.Json;

namespace SimplifiedStock.IntegrationTests;

public class TransactionFlowTests : IntegrationTestBase
{
    public TransactionFlowTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Buy_Should_Create_Wallet_Decrease_Bank_And_Add_Stock_To_Wallet()
    {
        //Arrange
        await SeedStock();

        var walletId = Guid.NewGuid();

        //Act
        var response = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "buy" });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        WalletResponse? wallet = await Client.GetFromJsonAsync<WalletResponse>(
            $"/wallets/{walletId}");

        //Assert
        wallet.Should().NotBeNull();
        wallet.Stocks.Should().ContainSingle(x => x.Name == "AAPL");
    }

    [Fact]
    public async Task Sell_Should_Decrease_Wallet_Stock_And_Add_To_Bank()
    {
        //Arrange
        await SeedStock();

        var walletId = Guid.NewGuid();

        //Act

        //Firstly 2 buy stocks
        var tasks = Enumerable.Range(0, 2)
           .Select(_ =>
               Client.PostAsJsonAsync(
                   $"/wallets/{walletId}/stocks/AAPL",
                   new { type = "buy" })
           );

        await Task.WhenAll(tasks);

        //Sell stock 1 stock
        var response = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "sell" });

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        WalletResponse? wallet = await Client.GetFromJsonAsync<WalletResponse>(
           $"/wallets/{walletId}");

        //Assert
        wallet.Should().NotBeNull();
        wallet.Stocks.Should().ContainSingle(x => x.Name == "AAPL" && x.Quantity == 1);
    }
}

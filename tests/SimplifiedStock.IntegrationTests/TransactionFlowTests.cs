using FluentAssertions;
using SimplifiedStock.IntegrationTests.Base;
using SimplifiedStock.Services.DTO.Stock;
using SimplifiedStock.Services.DTO.Wallet;
using System.Net;
using System.Net.Http.Json;

namespace SimplifiedStock.IntegrationTests;

[Collection("Integration")]
public class TransactionFlowTests : IntegrationTestBase
{
    public TransactionFlowTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Buy_Should_Create_Wallet_Decrease_Bank_And_Add_Stock_To_Wallet()
    {
        //Arrange
        await ResetDb();
        await SeedStock();

        var walletId = Guid.NewGuid();

        //Act
        var response = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "buy" });

        WalletResponse? wallet = await Client.GetFromJsonAsync<WalletResponse>(
            $"/wallets/{walletId}");

        var bank = await Client.GetFromJsonAsync<StockResponse>($"/stocks");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        wallet.Should().NotBeNull();
        bank.Should().NotBeNull();
        wallet.Stocks.Should().ContainSingle(x => x.Name == "AAPL");
        bank.Stocks.First(x => x.Name == "AAPL").Quantity.Should().Be(99);
    }

    [Fact]
    public async Task Sell_Should_Decrease_Wallet_Stock_And_Add_To_Bank()
    {
        //Arrange
        await ResetDb();
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

        WalletResponse? wallet = await Client.GetFromJsonAsync<WalletResponse>(
           $"/wallets/{walletId}");

        var bank = await Client.GetFromJsonAsync<StockResponse>($"/stocks");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        wallet.Should().NotBeNull();
        bank.Should().NotBeNull();
        wallet.Stocks.Should().ContainSingle(x => x.Name == "AAPL" && x.Quantity == 1);
        bank.Stocks.First(x => x.Name == "AAPL").Quantity.Should().Be(99);
    }

    [Fact]
    public async Task Buy_Should_Return_NotFound_When_Stock_Does_Not_Exist()
    {
        //Arrange
        await ResetDb();

        Guid walletId = Guid.NewGuid();

        //Act
        var response = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "buy" });

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Buy_Should_Return_BadRequest_When_Stock_Quantity_Is_Zero()
    {
        // Arrange
        await ResetDb();
        await SeedStock(quantity: 0);

        var walletId = Guid.NewGuid();

        // Act
        var response = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "buy" });

        var bank = await Client.GetFromJsonAsync<StockResponse>("/stocks");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        bank!.Stocks.First(x => x.Name == "AAPL").Quantity.Should().Be(0);
    }

    [Fact]
    public async Task Sell_Should_Return_BadRequest_When_Wallet_Does_Not_Have_Stock()
    {
        // Arrange
        await ResetDb();
        await SeedStock();

        var walletId = Guid.NewGuid();

        // Act
        var response = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "sell" });

        var bank = await Client.GetFromJsonAsync<StockResponse>("/stocks");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //bank quantity shouldnt change
        bank!.Stocks.First(x => x.Name == "AAPL").Quantity.Should().Be(100);
    }

    [Fact]
    public async Task Operation_Should_Return_OK_When_Successful()
    {
        // Arrange
        await ResetDb();
        await SeedStock();

        var walletId = Guid.NewGuid();

        // Act
        var response = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "buy" });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Buy_Should_Create_Wallet_When_Not_Exists()
    {
        // Arrange
        await ResetDb();
        await SeedStock();

        var walletId = Guid.NewGuid();

        // Act
        var response = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "buy" });

        WalletResponse? wallet = await Client.GetFromJsonAsync<WalletResponse>(
            $"/wallets/{walletId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        wallet.Should().NotBeNull();
        wallet.Id.Should().Be(walletId);
        wallet.Stocks.Should().ContainSingle(x => x.Name == "AAPL" && x.Quantity == 1);
    }
}

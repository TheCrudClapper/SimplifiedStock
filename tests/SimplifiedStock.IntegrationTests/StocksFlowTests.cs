using FluentAssertions;
using SimplifiedStock.IntegrationTests.Base;
using SimplifiedStock.Services.DTO.Stock;
using System.Net.Http.Json;

namespace SimplifiedStock.IntegrationTests;

[Collection("Integration")]
public class StocksFlowTests : IntegrationTestBase
{
    public StocksFlowTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Post_Stocks_Should_Override_Bank_State()
    {
        // Arrange
        await ResetDb();

        await Client.PostAsJsonAsync("/stocks", new
        {
            stocks = new[] { new { name = "AAPL", quantity = 10 } }
        });

        // Act
        await Client.PostAsJsonAsync("/stocks", new
        {
            stocks = new[] { new { name = "AAPL", quantity = 5 } }
        });

        var bank = await Client.GetFromJsonAsync<StockResponse>("/stocks");

        // Assert
        bank.Should().NotBeNull();
        bank.Stocks.Should().ContainSingle(x => x.Name == "AAPL" && x.Quantity == 5);
    }

    [Fact]
    public async Task Get_Stocks_Should_Return_Current_Bank_State()
    {
        // Arrange
        await ResetDb();
        await SeedStock(quantity: 42);

        // Act
        var bank = await Client.GetFromJsonAsync<StockResponse>("/stocks");

        // Assert
        bank!.Stocks.Should().ContainSingle(x => x.Name == "AAPL" && x.Quantity == 42);
    }
}

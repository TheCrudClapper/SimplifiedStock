using FluentAssertions;
using SimplifiedStock.IntegrationTests.Base;
using SimplifiedStock.Services.DTO.Stock;
using System.Net.Http.Json;

namespace SimplifiedStock.IntegrationTests;

public class ConcurencyTests : IntegrationTestBase
{
    public ConcurencyTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Concurrent_Buys_Should_Not_Corrupt_Stock()
    {
        //Arrange
        await SeedStock();

        var walletId = Guid.NewGuid();
        var walletId2 = Guid.NewGuid();

        //Act
        var tasks1 = Enumerable.Range(0, 20)
            .Select(_ =>
                Client.PostAsJsonAsync(
                    $"/wallets/{walletId}/stocks/AAPL",
                    new { type = "buy" })
            );

        var tasks2 = Enumerable.Range(0, 20)
            .Select(_ =>
                Client.PostAsJsonAsync(
                    $"/wallets/{walletId2}/stocks/AAPL",
                    new { type = "buy" })
            );

        await Task.WhenAll(tasks1.Concat(tasks2));

        StockResponse? bank = await Client.GetFromJsonAsync<StockResponse>("/stocks");

        //Assert
        bank.Should().NotBeNull();
        bank.Stocks.First(x => x.Name == "AAPL").Quantity.Should().Be(60);
    }
}

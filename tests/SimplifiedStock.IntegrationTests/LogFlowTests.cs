using FluentAssertions;
using SimplifiedStock.Domain.Entities.Enums;
using SimplifiedStock.IntegrationTests.Base;
using SimplifiedStock.Services.DTO.AuditLog;
using SimplifiedStock.Services.DTO.Enums;
using System.Net.Http.Json;

namespace SimplifiedStock.IntegrationTests;

[Collection("Integration")]
public class LogFlowTests : IntegrationTestBase
{
    public LogFlowTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Log_Should_Return_Only_Successfull_Operations()
    {

        // Arrange
        await ResetDb();
        await SeedStock();

        Guid walletId = Guid.NewGuid();

        // Act

        //Success - buy
        var res1 = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "buy" });

        //Success - sell
        var res2 = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/AAPL",
            new { type = "sell" });

        //Failure - stock does not exist
        var res3 = await Client.PostAsJsonAsync(
            $"/wallets/{walletId}/stocks/INVALID",
            new { type = "buy" });

        // Assert status codes
        res1.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        res2.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        res3.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        // Act - get log
        var response = await Client.GetFromJsonAsync<LogResponse>("/log");

        // Assert
        response.Should().NotBeNull();
        response.Log.Should().HaveCount(2);
        response.Log.First().Type.Should().Be(OperationTypeDto.Buy);
        response.Log.Last().Type.Should().Be(OperationTypeDto.Sell);
        response.Log.Select(x => x.WalletId)
            .Should().OnlyContain(id => id == walletId);
    }
}

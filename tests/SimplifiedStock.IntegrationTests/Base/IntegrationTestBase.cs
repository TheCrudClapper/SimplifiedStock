using System.Net.Http.Json;

namespace SimplifiedStock.IntegrationTests.Base;

public class IntegrationTestBase 
    : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly HttpClient Client;
    protected IntegrationTestBase(CustomWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
    }

    public async Task ResetDb()
    {
        await Client.PostAsync("/test/reset", null);
    }

    protected async Task SeedStock(int quantity = 100)
    {
        await Client.PostAsJsonAsync("/stocks", new
        {
            stocks = new[]
            {
                new { name = "AAPL", quantity }
            }
        });
    }
}

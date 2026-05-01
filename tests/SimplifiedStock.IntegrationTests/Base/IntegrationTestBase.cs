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

    protected async Task SeedStock()
    {
        await Client.PostAsJsonAsync("/stocks", new
        {
            stocks = new[]
            {
                new { name = "AAPL", quantity = 100 }
            }
        });
    }
}

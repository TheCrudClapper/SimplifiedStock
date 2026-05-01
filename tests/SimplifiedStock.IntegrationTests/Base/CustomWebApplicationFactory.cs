using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Services.ServiceContracts;
namespace SimplifiedStock.IntegrationTests.Base;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices((context, services) =>
        {
            //removing dev/prod database context
            var descriptor = services.SingleOrDefault(
              d => d.ServiceType == typeof(DbContextOptions<StockDatabaseContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            var configuration = context.Configuration;

            //add test db context
            services.AddDbContext<StockDatabaseContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("TestDb"),
                    x => x.MigrationsAssembly("SimplifiedStock.Infrastructure"));
            });

            //database cleaner service, not working in prod/dev
            services.AddScoped<ITestDatabaseCleaner, TestDatabaseCleaner>();
        });
    }

}


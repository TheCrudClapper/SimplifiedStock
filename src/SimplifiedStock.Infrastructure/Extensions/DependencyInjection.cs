using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplifiedStock.Infrastructure.Contexts;

namespace SimplifiedStock.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StockDatabaseContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Default"),
                x => x.MigrationsAssembly("SimplifiedStock.Infrastructure"));
        });
        return services;
    }  
}

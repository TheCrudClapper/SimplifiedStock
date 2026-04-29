using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplifiedStock.Services.ServiceContracts;
using SimplifiedStock.Services.Services;

namespace SimplifiedStock.Services.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ILogService, LogService>();
        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using SimplifiedStock.Domain.Entities;
namespace SimplifiedStock.Infrastructure.Contexts;

public class StockDatabaseContext : DbContext
{
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<BankStock> BankStocks { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletStock> WalletStocks { get; set; }

    public StockDatabaseContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankStock>()
            .HasKey(bs => bs.Name);

        modelBuilder.Entity<AuditLog>()
            .Property(al => al.Type)
            .HasConversion<string>()
            .IsRequired();

        modelBuilder.Entity<WalletStock>()
            .HasKey(ws => new { ws.WalletId, ws.Name });

        modelBuilder.Entity<WalletStock>()
            .HasOne(ws => ws.Wallet)
            .WithMany(w => w.WalletStocks)
            .HasForeignKey(ws => ws.WalletId);

        modelBuilder.Entity<Wallet>()
            .HasKey(w => w.Id);

        modelBuilder.Entity<AuditLog>()
            .HasKey(al => al.Id);
    }

}

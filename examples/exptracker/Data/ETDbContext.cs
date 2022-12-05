using System.Text.Json;
using FastCrud.Persistence.EFCore.Data;
using FastFinance.FastCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExpenseTracker.Data;

public class ETDbContext : BaseDbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Entry> Entries => Set<Entry>();

    public ETDbContext(DbContextOptions<ETDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.General);

        modelBuilder.Entity<Account>().Property(x => x.Credits)
            .HasColumnType("BLOB")
            .HasConversion(
                v => JsonSerializer.Serialize(v, options),
                v => JsonSerializer.Deserialize<Dictionary<string, decimal>>(v, options)!,
                ValueComparer.CreateDefault(typeof(Dictionary<string, decimal>), true));
        modelBuilder.Entity<Account>().Property(x => x.Debits)
            .HasColumnType("BLOB")
            .HasConversion(
                v => JsonSerializer.Serialize(v, options),
                v => JsonSerializer.Deserialize<Dictionary<string, decimal>>(v, options)!,
                ValueComparer.CreateDefault(typeof(Dictionary<string, decimal>), true));
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("postgres");

        modelBuilder?.Entity<Transaction>(entity =>
        {
            entity.ToTable(name: "transactions", schema: "postgres");

            entity.HasKey(x => x.TransactionId);

            entity.Property(e => e.TransactionId)
                .HasColumnName("transaction_id");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.Property(e => e.Date)
                .HasColumnName("date");

            entity.Property(e => e.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Category)
                .HasColumnName("category")
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(500);

            entity.Property(e => e.Merchant)
                .HasColumnName("merchant")
                .HasMaxLength(200);

            entity.HasIndex(t => new { t.UserId, t.Amount })
                .HasDatabaseName("IX_Transactions_UserId_Amount");

            entity.HasIndex(t => t.Category)
                .HasDatabaseName("IX_Transactions_Category_Count")
                .IncludeProperties(t => new { t.TransactionId });

            entity.HasIndex(t => new { t.UserId, t.Amount })
                .HasDatabaseName("IX_Transactions_UserId_Amount_Expense")
                .HasFilter("amount < 0");
        });
    }
}

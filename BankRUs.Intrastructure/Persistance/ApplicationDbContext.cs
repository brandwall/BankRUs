using BankRUs.Domain.Entities;
using BankRUs.Intrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankRUs.Intrastructure.Persistance;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BankAccount>(builder =>
        {
            builder.Property(x => x.Balance)
              .HasPrecision(18, 2);

            builder
                .HasIndex(b => b.AccountNumber)
                .IsUnique();
        });

        // Skapar koppling så att ett BankAccount har en ApplicationUser (one to many - en User kan ha flera Konton)
        builder.Entity<BankAccount>().
            HasOne<ApplicationUser>().
            WithMany().
            HasForeignKey(b => b.UserId);

        builder.Entity<Transaction>()
            .OwnsOne(d => d.Currency, currencyBuilder =>
            {
                currencyBuilder.Property(c => c.Code)
                .HasColumnName("Currency")
                .HasMaxLength(3);
            })
            .HasOne<BankAccount>()
            .WithMany()
            .HasForeignKey(d => d.BankAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        // Istället för att omvandla enum TransactionType till int så sparar den värdena som strängar
        builder.Entity<Transaction>()
            .Property(t => t.TransactionType)
            .HasConversion<string>()
            .HasMaxLength(20);
    }

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
}


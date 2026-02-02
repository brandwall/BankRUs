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

        builder.Entity<BankAccount>().
            HasOne<ApplicationUser>().
            WithMany().
            HasForeignKey(b => b.UserId);

        builder.Entity<Deposit>()
            .OwnsOne(d => d.Currency, currencyBuilder =>
            {
                currencyBuilder.Property(c => c.Code)
                .HasColumnName("Currency")
                .HasMaxLength(3);
            })
            .HasOne<BankAccount>()
            .WithMany()
            .HasForeignKey(d => d.BankAccountId);
    }

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<Deposit> Deposits => Set<Deposit>();
}


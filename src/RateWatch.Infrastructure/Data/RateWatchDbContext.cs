using Microsoft.EntityFrameworkCore;
using RateWatch.Domain.DTOs;
using RateWatch.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;

namespace RateWatch.Infrastructure.Data;

public class RateWatchDbContext(DbContextOptions<RateWatchDbContext> options) : DbContext(options)
{
    public DbSet<ExchangeRateRecord> ExchangeRateRecords => Set<ExchangeRateRecord>();
    public DbSet<SystemState> SystemStates => Set<SystemState>();
    public DbSet<Currency> Currencies => Set<Currency>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var now = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => c.Code).IsUnique();
            entity.Property(c => c.Code).HasMaxLength(3).IsRequired();
            entity.Property(c => c.Description).IsRequired();
            entity.Property(c => c.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<Currency>().HasData(
            new Currency { Id = 1, Code = "EUR", Description = "Euro", IsActive = true, UpdatedAt = now },
            new Currency { Id = 2, Code = "USD", Description = "US Dollar", IsActive = true, UpdatedAt = now },
            new Currency { Id = 3, Code = "JPY", Description = "Japanese Yen", IsActive = true, UpdatedAt = now },
            new Currency { Id = 4, Code = "BGN", Description = "Bulgarian Lev", IsActive = true, UpdatedAt = now },
            new Currency { Id = 5, Code = "CZK", Description = "Czech Koruna", IsActive = true, UpdatedAt = now },
            new Currency { Id = 6, Code = "DKK", Description = "Danish Krone", IsActive = true, UpdatedAt = now },
            new Currency { Id = 7, Code = "GBP", Description = "Pound Sterling", IsActive = true, UpdatedAt = now },
            new Currency { Id = 8, Code = "HUF", Description = "Hungarian Forint", IsActive = true, UpdatedAt = now },
            new Currency { Id = 9, Code = "PLN", Description = "Polish Zloty", IsActive = true, UpdatedAt = now },
            new Currency { Id = 10, Code = "RON", Description = "Romanian Leu", IsActive = true, UpdatedAt = now },
            new Currency { Id = 11, Code = "SEK", Description = "Swedish Krona", IsActive = true, UpdatedAt = now },
            new Currency { Id = 12, Code = "CHF", Description = "Swiss Franc", IsActive = true, UpdatedAt = now },
            new Currency { Id = 13, Code = "ISK", Description = "Icelandic Krona", IsActive = true, UpdatedAt = now },
            new Currency { Id = 14, Code = "NOK", Description = "Norwegian Krone", IsActive = true, UpdatedAt = now },
            new Currency { Id = 15, Code = "TRY", Description = "Turkish Lira", IsActive = true, UpdatedAt = now },
            new Currency { Id = 16, Code = "AUD", Description = "Australian Dollar", IsActive = true, UpdatedAt = now },
            new Currency { Id = 17, Code = "BRL", Description = "Brazilian Real", IsActive = true, UpdatedAt = now },
            new Currency { Id = 18, Code = "CAD", Description = "Canadian Dollar", IsActive = true, UpdatedAt = now },
            new Currency { Id = 19, Code = "CNY", Description = "Chinese Yuan", IsActive = true, UpdatedAt = now },
            new Currency { Id = 20, Code = "HKD", Description = "Hong Kong Dollar", IsActive = true, UpdatedAt = now },
            new Currency { Id = 21, Code = "IDR", Description = "Indonesian Rupiah", IsActive = true, UpdatedAt = now },
            new Currency { Id = 22, Code = "ILS", Description = "Israeli Shekel", IsActive = true, UpdatedAt = now },
            new Currency { Id = 23, Code = "INR", Description = "Indian Rupee", IsActive = true, UpdatedAt = now },
            new Currency { Id = 24, Code = "KRW", Description = "South Korean Won", IsActive = true, UpdatedAt = now },
            new Currency { Id = 25, Code = "MXN", Description = "Mexican Peso", IsActive = true, UpdatedAt = now },
            new Currency { Id = 26, Code = "MYR", Description = "Malaysian Ringgit", IsActive = true, UpdatedAt = now },
            new Currency { Id = 27, Code = "NZD", Description = "New Zealand Dollar", IsActive = true, UpdatedAt = now },
            new Currency { Id = 28, Code = "PHP", Description = "Philippine Peso", IsActive = true, UpdatedAt = now },
            new Currency { Id = 29, Code = "SGD", Description = "Singapore Dollar", IsActive = true, UpdatedAt = now },
            new Currency { Id = 30, Code = "THB", Description = "Thai Baht", IsActive = true, UpdatedAt = now },
            new Currency { Id = 31, Code = "ZAR", Description = "South African Rand", IsActive = true, UpdatedAt = now },
            new Currency { Id = 32, Code = "HRK", Description = "Croatian Kuna", IsActive = false, UpdatedAt = now },
            new Currency { Id = 33, Code = "RUB", Description = "Russian Ruble", IsActive = false, UpdatedAt = now },
            new Currency { Id = 34, Code = "LTL", Description = "Lithuanian Litas", IsActive = false, UpdatedAt = now },
            new Currency { Id = 35, Code = "LVL", Description = "Latvian Lats", IsActive = false, UpdatedAt = now },
            new Currency { Id = 36, Code = "EEK", Description = "Estonian Kroon", IsActive = false, UpdatedAt = now },
            new Currency { Id = 37, Code = "SKK", Description = "Slovak Koruna", IsActive = false, UpdatedAt = now },
            new Currency { Id = 38, Code = "CYP", Description = "Cypriot Pound", IsActive = false, UpdatedAt = now },
            new Currency { Id = 39, Code = "MTL", Description = "Maltese Lira", IsActive = false, UpdatedAt = now },
            new Currency { Id = 40, Code = "SIT", Description = "Slovenian Tolar", IsActive = false, UpdatedAt = now },
            new Currency { Id = 41, Code = "ROL", Description = "Old Romanian Leu", IsActive = false, UpdatedAt = now },
            new Currency { Id = 42, Code = "TRL", Description = "Old Turkish Lira", IsActive = false, UpdatedAt = now }
        );

        modelBuilder.Entity<ExchangeRateRecord>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Date).IsRequired();
            entity.Property(x => x.Rate).HasColumnType("numeric(18,6)");
            entity.Property(x => x.UpdatedAt).IsRequired();

            entity.HasOne(r => r.FromCurrency)
                .WithMany(c => c.FromRates)
                .HasForeignKey(r => r.FromCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.ToCurrency)
                .WithMany(c => c.ToRates)
                .HasForeignKey(r => r.ToCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(x => new { x.Date, x.FromCurrencyId, x.ToCurrencyId }).IsUnique();
        });

        modelBuilder.Entity<SystemState>(entity =>
        {
            entity.HasKey(e => e.Key);
        });
    }
}

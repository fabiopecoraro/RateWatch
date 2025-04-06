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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExchangeRateRecord>(entity =>
        {
            entity.HasKey(x => x.Id);

            //entity.Property(x => x.Date)
            //    .HasConversion<DateOnlyConverter>();

            entity.HasIndex(x => new { x.Date, x.FromCurrency, x.ToCurrency })
                .IsUnique();

            entity.Property(x => x.Rate)
                .HasColumnType("numeric(18,6)");
        });
    }
}

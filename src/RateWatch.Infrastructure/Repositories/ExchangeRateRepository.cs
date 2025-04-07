using Microsoft.EntityFrameworkCore;
using RateWatch.Application.Interfaces;
using RateWatch.Domain.Entities;
using RateWatch.Infrastructure.Data;

namespace RateWatch.Infrastructure.Repositories;

public class ExchangeRateRepository(RateWatchDbContext _db) : IExchangeRateRepository
{
    public async Task AddRatesAsync(IEnumerable<ExchangeRateRecord> rates, CancellationToken cancellationToken = default)
    {
        _db.ExchangeRateRecords.AddRange(rates);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ExchangeRateRecord>> GetRatesByDateAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        return await _db.ExchangeRateRecords
            .Where(r => r.Date == date)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsForDateAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        return await _db.ExchangeRateRecords
            .AnyAsync(r => r.Date == date, cancellationToken);
    }

    public async Task<List<DateOnly>> GetAvailableDatesAsync(CancellationToken ct = default)
    {
        return await _db.ExchangeRateRecords
            .Select(r => r.Date)
            .Distinct()
            .OrderByDescending(d => d)
            .ToListAsync(ct);
    }

}

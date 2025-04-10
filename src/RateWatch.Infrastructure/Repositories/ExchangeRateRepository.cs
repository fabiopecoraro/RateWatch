using Microsoft.EntityFrameworkCore;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.DTOs;
using RateWatch.Domain.Entities;
using RateWatch.Infrastructure.Data;

namespace RateWatch.Infrastructure.Repositories;

public class ExchangeRateRepository(RateWatchDbContext _db) : IExchangeRateRepository
{
    public async Task AddRatesAsync(IEnumerable<ExchangeRateRecord> rates, CancellationToken ct = default)
    {
        _db.ExchangeRateRecords.AddRange(rates);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<List<ExchangeRateRecord>> GetRatesByDateAsync(DateOnly date, CancellationToken ct = default)
    {
        return await _db.ExchangeRateRecords
            .Where(r => r.Date == date)
            .ToListAsync(ct);
    }

    public async Task<bool> ExistsForDateAsync(DateOnly date, CancellationToken ct = default)
    {
        return await _db.ExchangeRateRecords
            .AnyAsync(r => r.Date == date, ct);
    }

    public async Task<List<DateOnly>> GetAvailableDatesAsync(CancellationToken ct = default)
    {
        return await _db.ExchangeRateRecords
            .Select(r => r.Date)
            .Distinct()
            .OrderByDescending(d => d)
            .ToListAsync(ct);
    }

    public async Task<List<ExchangeRateTimePoint>> GetHistoryAsync(string toCurrencyCode, CancellationToken ct = default)
    {
        var toCurrencyId = await _db.Currencies
            .Where(c => c.Code == toCurrencyCode)
            .Select(c => c.Id)
            .FirstOrDefaultAsync(ct);

        if (toCurrencyId == 0)
            return [];

        return await _db.ExchangeRateRecords
            .Where(r => r.FromCurrency.Code == "EUR" && r.ToCurrencyId == toCurrencyId)
            .OrderBy(r => r.Date)
            .Select(r => new ExchangeRateTimePoint(r.Date, r.Rate))
            .ToListAsync(ct);
    }
}

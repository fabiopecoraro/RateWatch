using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.Entities;
using RateWatch.Domain.Models;
using RateWatch.Infrastructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RateWatch.Infrastructure.Repositories;

public class ExchangeRateRepository(RateWatchDbContext _db, IMapper _mapper) : IExchangeRateRepository
{
    public async Task AddRatesAsync(IEnumerable<ExchangeRateModel> rates, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var currencies = await _db.Currencies.ToListAsync(cancellationToken: ct);

        var records = rates.Select(r => new ExchangeRateRecord()
        {
            Date = r.Date,
            FromCurrencyId = currencies.Single(c => c.Code == r.FromCurrencyCode).Id,
            ToCurrencyId = currencies.Single(c => c.Code == r.ToCurrencyCode).Id,
            Rate = r.Rate,
            UpdatedAt = now
        }).ToList();

        _db.ExchangeRateRecords.AddRange(records);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<List<ExchangeRateModel>> GetRatesByDateAsync(DateOnly date, CancellationToken ct = default)
    {
        var records = await _db.ExchangeRateRecords
            .Include(r => r.FromCurrency)
            .Include(r => r.ToCurrency)
            .Where(r => r.Date == date)
            .ToListAsync(ct);

        return _mapper.Map<List<ExchangeRateModel>>(records);
    }

    public async Task<List<ExchangeRateModel>> GetLatestRatesAsync(CancellationToken ct = default)
    {
        var maxDate = await _db.ExchangeRateRecords
            .Select(r => r.Date)
            .OrderByDescending(d => d)
            .SingleOrDefaultAsync(cancellationToken: ct);

        if (maxDate == DateOnly.MinValue) return [];

        var records = await _db.ExchangeRateRecords
            .Include(r => r.FromCurrency)
            .Include(r => r.ToCurrency)
            .Where(r => r.Date == maxDate)
            .ToListAsync(ct);

        return _mapper.Map<List<ExchangeRateModel>>(records);
    }

    public async Task<bool> ExistsForDateAsync(DateOnly date, CancellationToken ct = default)
    {
        return await _db.ExchangeRateRecords
            .AnyAsync(r => r.Date == date, ct);
    }

    public async Task<List<DateOnly>> GetLast100AvailableDatesAsync(CancellationToken ct = default)
    {
        return await _db.ExchangeRateRecords
            .Select(r => r.Date)
            .Distinct()
            .OrderByDescending(d => d)
            .Take(100)
            .ToListAsync(ct);
    }

    public async Task<List<ExchangeRateModel>> GetHistoryAsync(string toCurrencyCode, CancellationToken ct = default)
    {
        var toCurrencyId = await _db.Currencies
            .Where(c => c.Code == toCurrencyCode)
            .Select(c => c.Id)
            .FirstOrDefaultAsync(ct);

        if (toCurrencyId == 0)
            return [];

        var records = await _db.ExchangeRateRecords
            .Where(r => r.FromCurrency!.Code == "EUR" && r.ToCurrencyId == toCurrencyId)
            .OrderBy(r => r.Date)
            .ToListAsync(ct);

        return _mapper.Map<List<ExchangeRateModel>>(records);
    }
}

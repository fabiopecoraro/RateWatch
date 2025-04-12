using Microsoft.EntityFrameworkCore;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.Entities;
using RateWatch.Infrastructure.Data;

namespace RateWatch.Infrastructure.Repositories;

public class CurrencyRepository(RateWatchDbContext _db) : ICurrencyRepository
{
    public async Task<List<Currency>> GetAllCurrenciesAsync(CancellationToken ct = default)
    {
        return await _db.Currencies
            .ToListAsync(ct);
    }

    public async Task<List<Currency>> GetActiveCurrenciesAsync(CancellationToken ct = default)
    {
        return await _db.Currencies
            .Where(c => c.IsActive)
            .OrderBy(c => c.Code)
            .ToListAsync(ct);
    }
}
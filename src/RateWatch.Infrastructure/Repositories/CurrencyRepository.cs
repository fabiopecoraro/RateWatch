using Microsoft.EntityFrameworkCore;
using RateWatch.Application.Interfaces;
using RateWatch.Domain.Entities;
using RateWatch.Infrastructure.Data;

namespace RateWatch.Infrastructure.Repositories;

public class CurrencyRepository(RateWatchDbContext _db) : ICurrencyRepository
{
    public async Task<Dictionary<string, int>> GetCurrencyMapAsync(CancellationToken ct = default)
    {
        return await _db.Currencies
            .ToDictionaryAsync(c => c.Code, c => c.Id, ct);
    }
}
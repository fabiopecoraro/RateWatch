using RateWatch.Domain.DTOs;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.Interfaces.Repositories;

public interface IExchangeRateRepository
{
    Task AddRatesAsync(IEnumerable<ExchangeRateRecord> rates, CancellationToken ct = default);
    Task<List<ExchangeRateRecord>> GetRatesByDateAsync(DateOnly date, CancellationToken ct = default);
    Task<bool> ExistsForDateAsync(DateOnly date, CancellationToken ct = default);
    Task<List<DateOnly>> GetAvailableDatesAsync(CancellationToken ct = default);
    Task<List<ExchangeRateTimePoint>> GetHistoryAsync(string toCurrencyCode, CancellationToken ct = default);
}

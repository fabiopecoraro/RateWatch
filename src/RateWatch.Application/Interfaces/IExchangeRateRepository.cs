using RateWatch.Domain.Entities;

namespace RateWatch.Application.Interfaces;

public interface IExchangeRateRepository
{
    Task AddRatesAsync(IEnumerable<ExchangeRateRecord> rates, CancellationToken cancellationToken = default);
    Task<List<ExchangeRateRecord>> GetRatesByDateAsync(DateOnly date, CancellationToken cancellationToken = default);
    Task<bool> ExistsForDateAsync(DateOnly date, CancellationToken cancellationToken = default);
    Task<List<DateOnly>> GetAvailableDatesAsync(CancellationToken ct = default);
}

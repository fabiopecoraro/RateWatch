using RateWatch.Domain.Models;

namespace RateWatch.Application.Interfaces.Repositories;

public interface IExchangeRateRepository
{
    Task AddRatesAsync(IEnumerable<ExchangeRateModel> rates, CancellationToken ct = default);
    Task<List<ExchangeRateModel>> GetLatestRatesAsync(CancellationToken ct = default);
    Task<List<ExchangeRateModel>> GetRatesByDateAsync(DateOnly date, CancellationToken ct = default);
    Task<bool> ExistsForDateAsync(DateOnly date, CancellationToken ct = default);
    Task<List<DateOnly>> GetLast100AvailableDatesAsync(CancellationToken ct = default);
    Task<List<ExchangeRateModel>> GetHistoryAsync(string toCurrencyCode, CancellationToken ct = default);
}

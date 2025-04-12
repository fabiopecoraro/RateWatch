using RateWatch.Domain.DTOs;

namespace RateWatch.Application.Interfaces.Repositories;

public interface IExchangeRateRepository
{
    Task AddRatesAsync(IEnumerable<ExchangeRateDto> rates, CancellationToken ct = default);
    Task<List<ExchangeRateDto>> GetLatestRatesAsync(CancellationToken ct = default);
    Task<List<ExchangeRateDto>> GetRatesByDateAsync(DateOnly date, CancellationToken ct = default);
    Task<bool> ExistsForDateAsync(DateOnly date, CancellationToken ct = default);
    Task<List<DateOnly>> GetLast100AvailableDatesAsync(CancellationToken ct = default);
    Task<List<ExchangeRateDto>> GetHistoryAsync(string toCurrencyCode, CancellationToken ct = default);
}

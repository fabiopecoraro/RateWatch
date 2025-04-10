using RateWatch.Application.DTOs;

namespace RateWatch.Application.Interfaces.ExternalServices;

public interface IRateFetcherService
{
    Task<ExchangeRateDay?> GetLatestRatesAsync(CancellationToken ct = default);
    Task<List<ExchangeRateDay>> GetHistoricalRatesAsync(CancellationToken ct = default);
}

using RateWatch.Domain.DTOs;

namespace RateWatch.Application.Interfaces;

public interface IRateFetcher
{
    Task<ExchangeRateDay?> GetLatestRatesAsync(CancellationToken cancellationToken = default);
}

using RateWatch.Domain.Entities;

namespace RateWatch.Application.Interfaces.Repositories;

public interface ICurrencyRepository
{
    Task<List<Currency>> GetAllCurrenciesAsync(CancellationToken ct = default);
    Task<List<Currency>> GetActiveCurrenciesAsync(CancellationToken ct = default);
}
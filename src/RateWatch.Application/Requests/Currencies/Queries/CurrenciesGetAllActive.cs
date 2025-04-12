using MediatR;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.Requests.Currencies.Queries;

public record CurrenciesGetAllActiveQuery : IRequest<List<Currency>>;

internal class CurrenciesGetAllActiveHandler(ICurrencyRepository _currencyRepository) : IRequestHandler<CurrenciesGetAllActiveQuery, List<Currency>>
{
    public async Task<List<Currency>> Handle(CurrenciesGetAllActiveQuery request, CancellationToken ct)
    {
        var allCurrency = await _currencyRepository.GetActiveCurrenciesAsync(ct);
        return allCurrency;
    }
}

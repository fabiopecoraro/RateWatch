using MediatR;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.Requests.Currencies.Queries;

public record CurrenciesGetAllQuery : IRequest<List<Currency>>;

internal class CurrenciesGetAllHandler(ICurrencyRepository _currencyRepository) : IRequestHandler<CurrenciesGetAllQuery, List<Currency>>
{
    public async Task<List<Currency>> Handle(CurrenciesGetAllQuery request, CancellationToken ct)
    {
        var allCurrency = await _currencyRepository.GetAllCurrenciesAsync(ct);
        return allCurrency;
    }
}

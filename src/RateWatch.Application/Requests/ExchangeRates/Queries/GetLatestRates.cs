using MediatR;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.Models;

namespace RateWatch.Application.Requests.ExchangeRates.Queries;

public record GetLatestRatesQuery : IRequest<List<ExchangeRateModel>>;

internal class GetLatestRatesHandler(IExchangeRateRepository _exchangeRateRepository) : IRequestHandler<GetLatestRatesQuery, List<ExchangeRateModel>>
{
    public async Task<List<ExchangeRateModel>> Handle(GetLatestRatesQuery request, CancellationToken ct)
    {
        var latestRates = await _exchangeRateRepository.GetLatestRatesAsync(ct);
        return latestRates;
    }
}

using MediatR;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.DTOs;

namespace RateWatch.Application.Requests.ExchangeRates.Queries;

public record GetLatestRatesQuery : IRequest<List<ExchangeRateDto>>;

internal class GetLatestRatesHandler(IExchangeRateRepository _exchangeRateRepository) : IRequestHandler<GetLatestRatesQuery, List<ExchangeRateDto>>
{
    public async Task<List<ExchangeRateDto>> Handle(GetLatestRatesQuery request, CancellationToken ct)
    {
        var latestRates = await _exchangeRateRepository.GetLatestRatesAsync(ct);
        return latestRates;
    }
}

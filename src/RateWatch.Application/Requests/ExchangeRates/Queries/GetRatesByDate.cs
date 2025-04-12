using MediatR;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.DTOs;

namespace RateWatch.Application.Requests.ExchangeRates.Queries;

public record GetRatesByDateQuery(DateOnly Date) : IRequest<List<ExchangeRateDto>>;

internal class GetRatesByDateHandler(IExchangeRateRepository _exchangeRateRepository) : IRequestHandler<GetRatesByDateQuery, List<ExchangeRateDto>>
{
    public async Task<List<ExchangeRateDto>> Handle(GetRatesByDateQuery request, CancellationToken ct)
    {
        var latestRates = await _exchangeRateRepository.GetRatesByDateAsync(request.Date, ct);
        return latestRates;
    }
}

using MediatR;
using RateWatch.Application.Interfaces.Repositories;

namespace RateWatch.Application.Requests.ExchangeRates.Queries;

public record GetLast100AvailableDatesQuery : IRequest<List<DateOnly>>;

internal class GetLast100AvailableDatesHandler(IExchangeRateRepository _exchangeRateRepository) : IRequestHandler<GetLast100AvailableDatesQuery, List<DateOnly>>
{
    public async Task<List<DateOnly>> Handle(GetLast100AvailableDatesQuery request, CancellationToken ct)
    {
        var latestRates = await _exchangeRateRepository.GetLast100AvailableDatesAsync(ct);
        return latestRates;
    }
}

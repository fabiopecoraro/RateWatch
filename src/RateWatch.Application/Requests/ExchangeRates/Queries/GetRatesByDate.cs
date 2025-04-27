using MediatR;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.Models;

namespace RateWatch.Application.Requests.ExchangeRates.Queries;

public record GetRatesByDateQuery(DateOnly Date) : IRequest<List<ExchangeRateModel>>;

internal class GetRatesByDateHandler(IExchangeRateRepository _exchangeRateRepository) : IRequestHandler<GetRatesByDateQuery, List<ExchangeRateModel>>
{
    public async Task<List<ExchangeRateModel>> Handle(GetRatesByDateQuery request, CancellationToken ct)
    {
        var latestRates = await _exchangeRateRepository.GetRatesByDateAsync(request.Date, ct);
        return latestRates;
    }
}

using MediatR;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.DTOs;

namespace RateWatch.Application.Requests.ExchangeRates.Queries;

public record ExchangeRatesGetHistoryQuery(string ToCurrency) : IRequest<List<ExchangeRateDto>>;

internal class ExchangeRatesGetHistoryHandler(IExchangeRateRepository _exchangeRateRepository) : IRequestHandler<ExchangeRatesGetHistoryQuery, List<ExchangeRateDto>>
{
    public async Task<List<ExchangeRateDto>> Handle(ExchangeRatesGetHistoryQuery request, CancellationToken ct)
    {
        var historyRates = await _exchangeRateRepository.GetHistoryAsync(request.ToCurrency, ct);
        return historyRates;
    }
}

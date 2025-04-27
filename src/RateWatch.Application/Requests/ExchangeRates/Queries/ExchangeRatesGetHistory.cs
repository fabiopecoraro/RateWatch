using MediatR;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.Models;

namespace RateWatch.Application.Requests.ExchangeRates.Queries;

public record ExchangeRatesGetHistoryQuery(string ToCurrency) : IRequest<List<ExchangeRateModel>>;

internal class ExchangeRatesGetHistoryHandler(IExchangeRateRepository _exchangeRateRepository) : IRequestHandler<ExchangeRatesGetHistoryQuery, List<ExchangeRateModel>>
{
    public async Task<List<ExchangeRateModel>> Handle(ExchangeRatesGetHistoryQuery request, CancellationToken ct)
    {
        var historyRates = await _exchangeRateRepository.GetHistoryAsync(request.ToCurrency, ct);
        return historyRates;
    }
}

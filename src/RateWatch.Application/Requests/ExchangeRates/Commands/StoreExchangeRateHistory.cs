using MediatR;
using RateWatch.Application.Interfaces.ExternalServices;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.DTOs;

namespace RateWatch.Application.Requests.ExchangeRates.Commands;

public record StoreExchangeRateHistoryCommand : IRequest<int>;

public class StoreExchangeRateHistory(
    IRateFetcherService _rateFetcherService,
    IExchangeRateRepository _exchangeRateRepository
    ) : IRequestHandler<StoreExchangeRateHistoryCommand, int>
{
    public async Task<int> Handle(StoreExchangeRateHistoryCommand request, CancellationToken ct)
    {
        var allDays = await _rateFetcherService.GetHistoricalRatesAsync(ct);

        var ratesToSave = new List<ExchangeRateDto>();
        foreach (var day in allDays)
        {
            if (await _exchangeRateRepository.ExistsForDateAsync(day.Date, ct))
                continue;

            var records = day.ExchangeRates.Select(r => new ExchangeRateDto()
            {
                Date = day.Date,
                FromCurrencyCode = r.FromCurrency,
                ToCurrencyCode = r.ToCurrency,
                Rate = r.Rate,
            }).ToList();

            ratesToSave.AddRange(records);
        }

        await _exchangeRateRepository.AddRatesAsync(ratesToSave, ct);

        return ratesToSave.Count;
    }
}

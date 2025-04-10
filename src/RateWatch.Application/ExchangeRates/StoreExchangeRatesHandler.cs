using MediatR;
using RateWatch.Application.Helpers;
using RateWatch.Application.Interfaces.ExternalServices;
using RateWatch.Application.Interfaces.Repositories;

namespace RateWatch.Application.ExchangeRates;

public class StoreExchangeRatesHandler(
    IRateFetcherService _rateFetcherService,
    IExchangeRateRepository _excangeRateRepository,
    ICurrencyRepository _currencyRepository) : IRequestHandler<StoreExchangeRatesCommand>
{
    public async Task Handle(StoreExchangeRatesCommand request, CancellationToken ct)
    {
        var day = await _rateFetcherService.GetLatestRatesAsync(ct);
        if (day is null || await _excangeRateRepository.ExistsForDateAsync(day.Date, ct))
            return;

        var currencyMap = await _currencyRepository.GetCurrencyMapAsync(ct);

        var records = ExchangeRateMapper.MapToRecords(day, currencyMap);
        await _excangeRateRepository.AddRatesAsync(records, ct);
    }
}

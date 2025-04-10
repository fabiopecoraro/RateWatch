using MediatR;
using RateWatch.Application.Helpers;
using RateWatch.Application.Interfaces.ExternalServices;
using RateWatch.Application.Interfaces.Repositories;

namespace RateWatch.Application.ExchangeRates;

public class StoreExchangeRateHistoryHandler(
    IRateFetcherService _rateFetcherService,
    IExchangeRateRepository _exchangeRateRepository,
    ICurrencyRepository _currencyRepository) : IRequestHandler<StoreExchangeRateHistoryCommand, int>
{
    public async Task<int> Handle(StoreExchangeRateHistoryCommand request, CancellationToken cr)
    {
        var allDays = await _rateFetcherService.GetHistoricalRatesAsync(cr);
        int importedCount = 0;

        foreach (var day in allDays)
        {
            if (await _exchangeRateRepository.ExistsForDateAsync(day.Date, cr))
                continue;
            
            var currencyMap = await _currencyRepository.GetCurrencyMapAsync(cr);

            var records = ExchangeRateMapper.MapToRecords(day, currencyMap);
            await _exchangeRateRepository.AddRatesAsync(records, cr);
            
            importedCount += records.Count;
        }

        return importedCount;
    }
}

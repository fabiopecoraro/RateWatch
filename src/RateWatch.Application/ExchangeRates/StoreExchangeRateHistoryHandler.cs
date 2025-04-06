using AutoMapper;
using MediatR;
using RateWatch.Application.Interfaces;
using RateWatch.Application.Services;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.ExchangeRates;

public class StoreExchangeRateHistoryHandler(
    IRateFetcher _fetcher,
    IExchangeRateRepository _repo,
    ICurrencyRepository _currencyRepository,
    ExchangeRateMapper _mapper) : IRequestHandler<StoreExchangeRateHistoryCommand, int>
{
    public async Task<int> Handle(StoreExchangeRateHistoryCommand request, CancellationToken cancellationToken)
    {
        var allDays = await _fetcher.GetHistoricalRatesAsync(cancellationToken);
        int importedCount = 0;

        foreach (var day in allDays)
        {
            if (await _repo.ExistsForDateAsync(day.Date, cancellationToken))
                continue;
            
            var currencyMap = await _currencyRepository.GetCurrencyMapAsync(cancellationToken);

            var records = _mapper.MapToRecords(day, currencyMap);
            await _repo.AddRatesAsync(records, cancellationToken);
            
            importedCount += records.Count;
        }

        return importedCount;
    }
}

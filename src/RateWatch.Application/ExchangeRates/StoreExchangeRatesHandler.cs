using AutoMapper;
using MediatR;
using RateWatch.Application.Interfaces;
using RateWatch.Application.Services;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.ExchangeRates;

public class StoreExchangeRatesHandler(
    IRateFetcher _fetcher,
    IExchangeRateRepository _repo,
    ICurrencyRepository _currencyRepository,
    ExchangeRateMapper _mapper) : IRequestHandler<StoreExchangeRatesCommand>
{
    public async Task Handle(StoreExchangeRatesCommand request, CancellationToken cancellationToken)
    {
        var day = await _fetcher.GetLatestRatesAsync(cancellationToken);
        if (day is null || await _repo.ExistsForDateAsync(day.Date, cancellationToken))
            return;

        var currencyMap = await _currencyRepository.GetCurrencyMapAsync(cancellationToken);

        var records = _mapper.MapToRecords(day, currencyMap);
        await _repo.AddRatesAsync(records, cancellationToken);
    }
}

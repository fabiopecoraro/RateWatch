using AutoMapper;
using MediatR;
using RateWatch.Application.Interfaces;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.ExchangeRates;

public class StoreExchangeRatesHandler(
    IRateFetcher _fetcher,
    IExchangeRateRepository _repo,
    IMapper _mapper) : IRequestHandler<StoreExchangeRatesCommand>
{
    public async Task Handle(StoreExchangeRatesCommand request, CancellationToken cancellationToken)
    {
        var day = await _fetcher.GetLatestRatesAsync(cancellationToken);
        if (day is null || await _repo.ExistsForDateAsync(day.Date, cancellationToken))
            return;

        var records = _mapper.Map<List<ExchangeRateRecord>>(day);
        await _repo.AddRatesAsync(records, cancellationToken);
    }
}

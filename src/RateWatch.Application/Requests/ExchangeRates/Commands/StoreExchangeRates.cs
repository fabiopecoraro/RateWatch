using MediatR;
using RateWatch.Application.Interfaces.ExternalServices;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.DTOs;

namespace RateWatch.Application.Requests.ExchangeRates.Commands;

public record StoreExchangeRatesCommand : IRequest;

public class StoreExchangeRates(
    IRateFetcherService _rateFetcherService,
    IExchangeRateRepository _excangeRateRepository
    ) : IRequestHandler<StoreExchangeRatesCommand>
{
    public async Task Handle(StoreExchangeRatesCommand request, CancellationToken ct)
    {
        var day = await _rateFetcherService.GetLatestRatesAsync(ct);
        if (day is null || await _excangeRateRepository.ExistsForDateAsync(day.Date, ct))
            return;

        var records = day.ExchangeRates.Select(r => new ExchangeRateModel()
        {
            Date = day.Date,
            FromCurrencyCode = r.FromCurrency,
            ToCurrencyCode = r.ToCurrency,
            Rate = r.Rate,
        }).ToList();

        await _excangeRateRepository.AddRatesAsync(records, ct);
    }
}

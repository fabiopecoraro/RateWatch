using RateWatch.Domain.DTOs;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.Services;

public class ExchangeRateMapper
{
    public List<ExchangeRateRecord> MapToRecords(
        ExchangeRateDay day,
        Dictionary<string, int> currencyMap)
    {
        var now = DateTime.UtcNow;

        return day.ExchangeRates.Select(rate =>
        {
            if (!currencyMap.TryGetValue(rate.FromCurrency, out var fromId))
                throw new InvalidOperationException($"Valuta non trovata: {rate.FromCurrency}");

            if (!currencyMap.TryGetValue(rate.ToCurrency, out var toId))
                throw new InvalidOperationException($"Valuta non trovata: {rate.ToCurrency}");

            return new ExchangeRateRecord
            {
                Date = day.Date,
                FromCurrencyId = fromId,
                ToCurrencyId = toId,
                Rate = rate.Rate,
                UpdatedAt = now
            };
        }).ToList();
    }
}

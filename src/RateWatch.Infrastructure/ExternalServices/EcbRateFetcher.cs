using System.Xml.Linq;
using System.Globalization;
using System.Xml.Serialization;
using System;
using System.Net.Http;
using RateWatch.Domain.ECB;
using RateWatch.Domain.ECB.Commons;
using RateWatch.Application.Interfaces;
using RateWatch.Domain.DTOs;

namespace RateWatch.Infrastructure.ExternalServices;

public class EcbRateFetcher(HttpClient _httpClient) : IRateFetcher
{
    public async Task<ExchangeRateDay?> GetLatestRatesAsync(CancellationToken cancellationToken = default)
    {
        var stream = await _httpClient.GetStreamAsync(EcbFileUrls.XmlLatestRates, cancellationToken);
        var serializer = new XmlSerializer(typeof(Envelope));
        var envelope = serializer.Deserialize(stream) as Envelope;

        if (envelope == null || envelope.Cube.Cubes.Count != 1)
            return null;

        var exchday = new ExchangeRateDay
        {
            Date = DateOnly.Parse(envelope.Cube.Cubes[0].Time),
            ExchangeRates = envelope.Cube.Cubes[0].Rates.Select(r => new ExchangeRate()
            {
                FromCurrency = "EUR",
                ToCurrency = r.Currency,
                Rate = decimal.Parse(r.Rate, CultureInfo.InvariantCulture)
            }).ToList()
        };

        return exchday;
    }

    public async Task<List<ExchangeRateDay>> GetHistoricalRatesAsync(CancellationToken ct = default)
    {
        var stream = await _httpClient.GetStreamAsync(EcbFileUrls.XmlHistoricalRates, ct);
        var serializer = new XmlSerializer(typeof(Envelope));
        var envelope = serializer.Deserialize(stream) as Envelope;

        if (envelope == null)
            return [];

        var result = envelope.Cube.Cubes
            .Where(c => !string.IsNullOrEmpty(c.Time))
            .Select(day => new ExchangeRateDay
            {
                Date = DateOnly.Parse(day.Time),
                ExchangeRates = day.Rates.Select(r => new ExchangeRate
                {
                    FromCurrency = "EUR",
                    ToCurrency = r.Currency,
                    Rate = decimal.Parse(r.Rate, CultureInfo.InvariantCulture)
                }).ToList()
            })
            .ToList();

        return result;
    }
}

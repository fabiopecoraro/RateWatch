using RateWatch.Application.Interfaces;
using RateWatch.Api.DTOs;
using RateWatch.Domain.DTOs;

namespace RateWatch.Api.Endpoints;

public static class RateEndpoints
{
    public static IEndpointRouteBuilder MapRateEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/rates/latest", async (IRateFetcher rateFetcher) =>
        {
            var data = await rateFetcher.GetLatestRatesAsync();
            if (data is null)
                return Results.NotFound("Dati BCE non disponibili al momento.");

            var response = new ExchangeRateResponse
            {
                Date = data.Date.ToString("yyyy-MM-dd"),
                Rates = data.ExchangeRates.Select(r => new ExchangeRateItem
                {
                    From = r.FromCurrency,
                    To = r.ToCurrency,
                    Rate = r.Rate
                }).ToList()
            };

            return TypedResults.Ok(response);
        })
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ExchangeRateResponse>();

        app.MapGet("/api/rates/{date}", async (DateOnly date, IExchangeRateRepository repo, ICurrencyRepository currencyRepo, CancellationToken ct) =>
        {
            var records = await repo.GetRatesByDateAsync(date, ct);
            if (records.Count == 0)
                return Results.NotFound();

            var currencyMap = await currencyRepo.GetCurrencyMapAsync(ct);
            var reverseMap = currencyMap.ToDictionary(kv => kv.Value, kv => kv.Key);

            var dto = new ExchangeRateDay
            {
                Date = date,
                ExchangeRates = records.Select(r => new ExchangeRate
                {
                    FromCurrency = reverseMap[r.FromCurrencyId],
                    ToCurrency = reverseMap[r.ToCurrencyId],
                    Rate = r.Rate
                }).ToList()
            };

            return Results.Ok(dto);
        })
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ExchangeRateDay>();

        app.MapGet("/api/currencies", async (ICurrencyRepository repo, CancellationToken ct) =>
        {
            var currencies = await repo.GetActiveCurrenciesAsync(ct);
            return Results.Ok(currencies.Select(c => new CurrencyResponse()
            {
                Code = c.Code,
                Description = c.Description
            }));
        }).Produces<CurrencyResponse>();


        app.MapGet("/api/rates/dates", async (IExchangeRateRepository repo, CancellationToken ct) =>
        {
            var dates = await repo.GetAvailableDatesAsync(ct);
            return Results.Ok(dates);
        });

        return app;
    }
}

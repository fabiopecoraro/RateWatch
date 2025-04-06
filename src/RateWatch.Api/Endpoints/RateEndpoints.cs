using RateWatch.Application.Interfaces;
using RateWatch.Api.DTOs;

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

            return Results.Ok(response);
        });

        return app;
    }
}

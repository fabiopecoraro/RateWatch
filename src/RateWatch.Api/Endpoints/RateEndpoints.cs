using MediatR;
using RateWatch.Api.ViewModels.Responses;
using RateWatch.Application.Requests.Currencies.Queries;
using RateWatch.Application.Requests.ExchangeRates.Queries;

namespace RateWatch.Api.Endpoints;

public static class RateEndpoints
{
    public static IEndpointRouteBuilder MapRateEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/rates/latest", async (IMediator mediator, CancellationToken ct) =>
        {
            var rates = await mediator.Send(new GetLatestRatesQuery(), ct);
            if (rates.Count == 0)
                return Results.NotFound();

            var response = new ExchangeRateResponse
            {
                Date = rates.First().Date.ToString("yyyy-MM-dd"),
                Rates = rates.Select(r => new ExchangeRateItem
                {
                    From = r.FromCurrencyCode,
                    To = r.ToCurrencyCode,
                    Rate = r.Rate
                }).ToList()
            };

            return Results.Ok(response);
        });

        app.MapGet("/api/rates/{date}", async (DateOnly date, IMediator mediator, CancellationToken ct) =>
        {
            var rates = await mediator.Send(new GetRatesByDateQuery(date), ct);
            if (rates.Count == 0)
                return Results.NotFound();

            var response = new ExchangeRateResponse
            {
                Date = rates.First().Date.ToString("yyyy-MM-dd"),
                Rates = rates.Select(r => new ExchangeRateItem
                {
                    From = r.FromCurrencyCode,
                    To = r.ToCurrencyCode,
                    Rate = r.Rate
                }).ToList()
            };

            return Results.Ok(response);
        });

        app.MapGet("/api/currencies", async (IMediator mediator, CancellationToken ct) =>
        {
            var currencies = await mediator.Send(new CurrenciesGetAllActiveQuery(), ct);

            return Results.Ok(currencies.Select(c => new CurrencyResponse()
            {
                Code = c.Code,
                Description = c.Description
            }));
        });

        app.MapGet("/api/rates/dates", async (IMediator mediator, CancellationToken ct) =>
        {
            var dates = await mediator.Send(new GetLast100AvailableDatesQuery(), ct);
            return Results.Ok(dates);
        });

        app.MapGet("/api/rates/history", async (string to, IMediator mediator, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(to))
                return Results.BadRequest("Missing currency code.");

            var history = await mediator.Send(new ExchangeRatesGetHistoryQuery(to.ToUpperInvariant()), ct);
            return Results.Ok(history);
        });

        return app;
    }
}

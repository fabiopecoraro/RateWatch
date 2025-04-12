using Microsoft.EntityFrameworkCore;
using RateWatch.Api.Endpoints;
using RateWatch.Application.Interfaces.ExternalServices;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Application.Requests.ExchangeRates.Commands;
using RateWatch.Infrastructure.BackgroundJobs;
using RateWatch.Infrastructure.Data;
using RateWatch.Infrastructure.ExternalServices.RateFetcher;
using RateWatch.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<RateWatchDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<IRateFetcherService, RateFetcherService>();

builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<ISystemStateRepository, SystemStateRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();

builder.Services.AddHostedService<ExchangeRateBackgroundService>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<StoreExchangeRatesCommand>());

builder.Services.AddAutoMapper(typeof(RateWatch.Infrastructure.Mapping.DbToDomainProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapRateEndpoints();

app.Run();
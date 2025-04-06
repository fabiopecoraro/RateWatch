using Microsoft.EntityFrameworkCore;
using RateWatch.Api.Endpoints;
using RateWatch.Application.ExchangeRates;
using RateWatch.Application.Interfaces;
using RateWatch.Infrastructure.Data;
using RateWatch.Infrastructure.ExternalServices;
using RateWatch.Infrastructure.Hosted;
using RateWatch.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<RateWatchDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<IRateFetcher, EcbRateFetcher>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

builder.Services.AddHostedService<ExchangeRateBackgroundService>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<StoreExchangeRatesCommand>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapRateEndpoints();

app.Run();
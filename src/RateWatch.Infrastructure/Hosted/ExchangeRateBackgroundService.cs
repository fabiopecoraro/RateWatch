using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RateWatch.Application.ExchangeRates;

namespace RateWatch.Infrastructure.Hosted;
public class ExchangeRateBackgroundService(IMediator _mediator, ILogger<ExchangeRateBackgroundService> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("HostedService avviato.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _mediator.Send(new StoreExchangeRatesCommand(), stoppingToken);
                _logger.LogInformation("Tassi aggiornati.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il salvataggio dei tassi.");
            }

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}

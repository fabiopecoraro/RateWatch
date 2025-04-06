using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RateWatch.Application.ExchangeRates;
using RateWatch.Application.Interfaces;

namespace RateWatch.Infrastructure.Hosted;
public class ExchangeRateBackgroundService(IServiceProvider _services, ILogger<ExchangeRateBackgroundService> _logger) : BackgroundService
{
    private const string HISTORY_FLAG = "ExchangeRateHistoryImported";

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Avvio servizio ExchangeRateBackgroundService...");

        try
        {
            using var scope = _services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var systemState = scope.ServiceProvider.GetRequiredService<ISystemStateRepository>();

            var alreadyImported = await systemState.IsFlagSetAsync(HISTORY_FLAG, cancellationToken);

            if (!alreadyImported)
            {
                _logger.LogInformation("Caricamento storico dei tassi in corso...");
                var imported = await mediator.Send(new StoreExchangeRateHistoryCommand(), cancellationToken);
                _logger.LogInformation("Storico completato: {Count} tassi importati.", imported);

                await systemState.SetFlagAsync(HISTORY_FLAG, true, cancellationToken);
            }
            else
            {
                _logger.LogInformation("Storico già caricato, nessuna azione necessaria.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante l'import dello storico.");
        }

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("HostedService avviato.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _services.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                _logger.LogInformation("Esecuzione fetch giornaliero...");
                await mediator.Send(new StoreExchangeRatesCommand(), stoppingToken);
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

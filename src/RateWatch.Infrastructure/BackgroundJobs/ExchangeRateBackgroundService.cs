using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RateWatch.Application.Requests.ExchangeRates.Commands;
using RateWatch.Application.Requests.SystemStates.Commands;
using RateWatch.Application.Requests.SystemStates.Queries;

namespace RateWatch.Infrastructure.BackgroundJobs;
public class ExchangeRateBackgroundService(IServiceProvider _services, ILogger<ExchangeRateBackgroundService> _logger) : BackgroundService
{
    private const string HISTORY_FLAG = "ExchangeRateHistoryImported";

    public override async Task StartAsync(CancellationToken ct)
    {
        _logger.LogInformation("Avvio servizio ExchangeRateBackgroundService...");

        try
        {
            using var scope = _services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var alreadyImported = await mediator.Send(new SystemStateGetFlagStatusQuery(HISTORY_FLAG), ct);

            if (!alreadyImported)
            {
                _logger.LogInformation("Caricamento storico dei tassi in corso...");
                var imported = await mediator.Send(new StoreExchangeRateHistoryCommand(), ct);
                _logger.LogInformation("Storico completato: {Count} tassi importati.", imported);

                await mediator.Send(new SystemStateSetFlagCommand(HISTORY_FLAG, true), ct);
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

        await base.StartAsync(ct);
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("HostedService avviato.");

        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var scope = _services.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                _logger.LogInformation("Esecuzione fetch giornaliero...");
                await mediator.Send(new StoreExchangeRatesCommand(), ct);
                _logger.LogInformation("Tassi aggiornati.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il salvataggio dei tassi.");
            }

            await Task.Delay(TimeSpan.FromHours(24), ct);
        }
    }
}

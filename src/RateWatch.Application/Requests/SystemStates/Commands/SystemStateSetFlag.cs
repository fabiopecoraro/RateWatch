using MediatR;
using RateWatch.Application.Interfaces.Repositories;

namespace RateWatch.Application.Requests.SystemStates.Commands;

public record SystemStateSetFlagCommand(string Key, bool Flag) : IRequest
{
    public string Key { get; init; } = Key;
    public bool Flag { get; init; } = Flag;
}

internal class SystemStateSetFlag(ISystemStateRepository _systemStateRepository) : IRequestHandler<SystemStateSetFlagCommand>
{
    public async Task Handle(SystemStateSetFlagCommand request, CancellationToken ct)
    {
        await _systemStateRepository.SetFlagAsync(request.Key, request.Flag, ct);
    }
}

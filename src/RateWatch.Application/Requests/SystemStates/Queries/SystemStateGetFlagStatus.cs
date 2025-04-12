using MediatR;
using RateWatch.Application.Interfaces.Repositories;


namespace RateWatch.Application.Requests.SystemStates.Queries;

public record SystemStateGetFlagStatusQuery(string Key) : IRequest<bool>;

internal class SystemStateGetFlagStatusHandler(ISystemStateRepository _systemStateRepository) : IRequestHandler<SystemStateGetFlagStatusQuery, bool>
{
    public async Task<bool> Handle(SystemStateGetFlagStatusQuery request, CancellationToken ct)
    {
        return await _systemStateRepository.IsFlagSetAsync(request.Key, ct);
    }
}

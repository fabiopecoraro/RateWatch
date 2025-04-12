using RateWatch.Domain.Entities;

namespace RateWatch.Application.Interfaces.Repositories;

public interface ISystemStateRepository
{
    Task<bool> IsFlagSetAsync(string key, CancellationToken ct = default);
    Task SetFlagAsync(string key, bool isSet = true, CancellationToken ct = default);
}
using Microsoft.EntityFrameworkCore;
using RateWatch.Application.Interfaces.Repositories;
using RateWatch.Domain.Entities;
using RateWatch.Infrastructure.Data;

namespace RateWatch.Infrastructure.Repositories;

public class SystemStateRepository(RateWatchDbContext _db) : ISystemStateRepository
{
    public async Task<bool> IsFlagSetAsync(string key, CancellationToken ct = default)
    {
        var entry = await _db.SystemStates.FindAsync([key], ct);
        return entry?.IsSet == true;
    }

    public async Task SetFlagAsync(string key, bool isSet = true, CancellationToken ct = default)
    {
        var entry = await _db.SystemStates.FindAsync([key], ct);
        var now = DateTime.UtcNow;

        if (entry is null)
        {
            _db.SystemStates.Add(new SystemState { Key = key, IsSet = isSet, UpdatedAt = now });
        }
        else
        {
            entry.IsSet = isSet;
            entry.UpdatedAt = now;
        }

        await _db.SaveChangesAsync(ct);
    }

    public Task<SystemState?> GetAsync(string key, CancellationToken ct = default)
        => _db.SystemStates.FindAsync([key], ct).AsTask();
}


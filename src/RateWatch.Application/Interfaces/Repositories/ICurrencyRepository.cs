﻿using RateWatch.Domain.Entities;

namespace RateWatch.Application.Interfaces.Repositories;

public interface ICurrencyRepository
{
    Task<Dictionary<string, int>> GetCurrencyMapAsync(CancellationToken ct = default);
    Task<List<Currency>> GetActiveCurrenciesAsync(CancellationToken ct = default);
}
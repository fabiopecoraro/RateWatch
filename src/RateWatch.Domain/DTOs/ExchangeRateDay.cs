﻿namespace RateWatch.Domain.DTOs;

public class ExchangeRateDay
{
    public DateOnly Date {  get; set; }
    public List<ExchangeRate> ExchangeRates { get; set; } = [];
}

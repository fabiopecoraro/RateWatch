namespace RateWatch.Domain.DTOs;


public class ExchangeRateDto
{
    public DateOnly Date { get; set; }

    public string FromCurrencyCode { get; set; } = null!;

    public string ToCurrencyCode { get; set; } = null!;

    public decimal Rate { get; set; }
}

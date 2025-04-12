namespace RateWatch.Domain.Entities;


public class ExchangeRateRecord
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int FromCurrencyId { get; set; }

    public int ToCurrencyId { get; set; }

    public decimal Rate { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Currency? FromCurrency { get; set; }

    public Currency? ToCurrency { get; set; }
}

namespace RateWatch.Domain.Entities;

public class ExchangeRateRecord
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string FromCurrency { get; set; } = null!;
    public string ToCurrency { get; set; } = null!;
    public decimal Rate { get; set; }
}
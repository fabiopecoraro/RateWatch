namespace RateWatch.Domain.Entities;

public class Currency
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<ExchangeRateRecord>? FromRates { get; set; }
    public ICollection<ExchangeRateRecord>? ToRates { get; set; }
}

namespace RateWatch.Api.ViewModels.Responses;

public class ExchangeRateResponse
{
    public string Date { get; set; } = null!;
    public List<ExchangeRateItem> Rates { get; set; } = [];
}

public class ExchangeRateItem
{
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public decimal Rate { get; set; }
}

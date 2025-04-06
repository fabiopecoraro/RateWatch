using MediatR;

namespace RateWatch.Application.ExchangeRates;

public record StoreExchangeRateHistoryCommand : IRequest<int>;
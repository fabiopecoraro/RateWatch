using AutoMapper;
using RateWatch.Domain.DTOs;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.Mappings;

public class ExchangeRateMappingProfile : Profile
{
    public ExchangeRateMappingProfile()
    {
        //CreateMap<ExchangeRateDay, List<ExchangeRateRecord>>()
        //    .ConvertUsing((src, _, context) =>
        //    {
        //        return src.ExchangeRates.Select(rate => new ExchangeRateRecord
        //        {
        //            Date = src.Date,
        //            FromCurrency = rate.FromCurrency,
        //            ToCurrency = rate.ToCurrency,
        //            Rate = rate.Rate
        //        }).ToList();
        //    });
    }
}

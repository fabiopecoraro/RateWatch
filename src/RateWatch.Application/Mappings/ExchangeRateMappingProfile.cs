using AutoMapper;
using RateWatch.Domain.DTOs;
using RateWatch.Domain.Entities;

namespace RateWatch.Application.Mappings;

public class ExchangeRateMappingProfile : Profile
{
    public ExchangeRateMappingProfile()
    {
        //CreateMap<ExchangeRate, ExchangeRateRecord>()
        //    .ForMember(dest => dest.Id, opt => opt.Ignore())
        //    .ForMember(dest => dest.Date, opt => opt.Ignore())
        //    .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ExchangeRateDay, List<ExchangeRateRecord>>()
            .ConvertUsing((src, _, context) =>
            {
                return src.ExchangeRates.Select(rate => new ExchangeRateRecord
                {
                    Date = src.Date,
                    FromCurrency = rate.FromCurrency,
                    ToCurrency = rate.ToCurrency,
                    Rate = rate.Rate
                }).ToList();
            });
    }
}

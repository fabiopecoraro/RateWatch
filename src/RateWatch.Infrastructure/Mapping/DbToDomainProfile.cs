using AutoMapper;
using RateWatch.Domain.Entities;
using RateWatch.Domain.Models;

namespace RateWatch.Infrastructure.Mapping;

public class DbToDomainProfile : Profile
{
    public DbToDomainProfile()
    {
        CreateMap<Currency, CurrencyModel>();

        CreateMap<ExchangeRateRecord, ExchangeRateModel>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.FromCurrencyCode, opt => opt.MapFrom(src => src.FromCurrency!.Code))
            .ForMember(dest => dest.ToCurrencyCode, opt => opt.MapFrom(src => src.ToCurrency!.Code))
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
        ;
    }
}

using AutoMapper;
using RateWatch.Domain.DTOs;
using RateWatch.Domain.Entities;

namespace RateWatch.Infrastructure.Mapping;

public class DbToDomainProfile : Profile
{
    public DbToDomainProfile()
    {
        CreateMap<Currency, CurrencyDto>();

        CreateMap<ExchangeRateRecord, ExchangeRateDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.FromCurrencyCode, opt => opt.MapFrom(src => src.FromCurrency!.Code))
            .ForMember(dest => dest.ToCurrencyCode, opt => opt.MapFrom(src => src.ToCurrency!.Code))
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
        ;
    }
}

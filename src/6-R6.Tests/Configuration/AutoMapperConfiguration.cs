using AutoMapper;
using R6.API.ViewModels;
using R6.Domain.Entities;
using R6.Services.DTO;

namespace R6.Tests.Configurations.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper GetConfiguration()
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Operator, OperatorDto>()
                    .ReverseMap();
            });

            return autoMapperConfig.CreateMapper();
        }
    }
}
using AutoMapper;
using Mango.Services.ProductAPIs.DTOs.ProductDtos;
using Mango.Services.ProductAPIs.Models;

namespace Mango.Services.ProductAPIs.Mapper
{
    public static class MapperConfigure
    {
        public static MapperConfiguration RegisterMappers()
        {
            var mappingConfiguration = new MapperConfiguration(configurationOption =>
            {
                configurationOption.CreateMap<ProductDto, ProductModel>();
                configurationOption.CreateMap<ProductDto, ProductModel>().ReverseMap();
            });
            return mappingConfiguration;
        }
    }
}

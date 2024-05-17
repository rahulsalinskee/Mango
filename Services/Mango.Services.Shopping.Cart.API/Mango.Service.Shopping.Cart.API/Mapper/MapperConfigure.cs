using AutoMapper;
using Mango.Service.Shopping.Cart.API.DTOs.ShoppingCart;
using Mango.Service.Shopping.Cart.API.Models;

namespace Mango.Service.Shopping.Cart.API.Mapper
{
    public static class MapperConfigure
    {
        public static MapperConfiguration RegisterMappers()
        {
            return new MapperConfiguration(configureOptions =>
            {
                configureOptions.CreateMap<ShoppingCartHeaderDto, CartHeader>().ReverseMap();
                configureOptions.CreateMap<ShoppingCartDetailsDto, CartDetails>().ReverseMap();
            });
        }
    }
}

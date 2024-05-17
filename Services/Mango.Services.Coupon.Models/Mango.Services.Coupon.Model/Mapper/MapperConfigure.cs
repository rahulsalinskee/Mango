using AutoMapper;
using Mango.Services.Coupon.Model.DTOs.CouponDtos;
using Mango.Services.Coupon.Model.Models;

namespace Mango.Services.Coupon.Model.Mapper
{
    public static class MapperConfigure
    {
        public static MapperConfiguration RegisterMappers()
        {
            var mappingConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<CouponDto, CouponModel>();
                configuration.CreateMap<CouponDto, CouponModel>().ReverseMap();
            });
            return mappingConfiguration;
        }
    }
}

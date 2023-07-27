using System;
using AutoMapper;
using Micro.Services.CouponAPI.Models;
using Micro.Services.CouponAPI.Models.Dto;

namespace Micro.Services.CouponAPI
{
    //Mapping to / from Dtos
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}

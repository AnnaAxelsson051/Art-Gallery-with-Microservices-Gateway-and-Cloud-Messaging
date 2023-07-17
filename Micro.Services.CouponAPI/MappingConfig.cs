using System;
using AutoMapper;
using Micro.Services.CouponAPI.Models;
using Micro.Services.CouponAPI.Models.Dto;

namespace Micro.Services.CouponAPI
{
    //Mapping between Coupon and Dto
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

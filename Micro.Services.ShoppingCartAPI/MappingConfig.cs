using System;
using AutoMapper;
using Micro.Services.ProductAPI;
using Micro.Services.ProductAPI.Models;
using Micro.Services.ProductAPI.Models.Dto;

namespace Micro.Services.ProductAPI
{
    //Mapping between Coupon and Dto
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap(); ;
            
            });
            return mappingConfig;
        }
    }
}

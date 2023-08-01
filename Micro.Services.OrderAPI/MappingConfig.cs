using System;
using AutoMapper;
using Micro.Services.ShoppingCartAPI;

namespace Micro.Services.OrderAPI
{
    //Mapping to / from Dtos
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
               
            });
            return mappingConfig;
        }
    }
}

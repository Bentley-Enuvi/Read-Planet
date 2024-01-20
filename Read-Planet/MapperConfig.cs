using Read_Planet.Models.DTOs;
using Read_Planet.Models;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;

namespace Read_Planet
{
    public class MapperConfig : Profile
    {
        public static MapperConfiguration MapsReg()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AppUser, AppUserDto>();
                config.CreateMap<AppUserDto, AppUser>();
            });
            return mapperConfig;
        }
        
    }
}

using AutoMapper;
using BloodConnector.WebAPI.DTOs;
using BloodConnector.WebAPI.Models;

namespace BloodConnector.WebAPI.App_Start
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.BloodGroup, opt => opt.MapFrom(u => u.BloodGroup.Symbole));
        }
    }
}
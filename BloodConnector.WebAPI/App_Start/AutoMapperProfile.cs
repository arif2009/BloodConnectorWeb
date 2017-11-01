using AutoMapper;
using BloodConnector.WebAPI.DTOs;
using BloodConnector.WebAPI.Helper;
using BloodConnector.WebAPI.Models;

namespace BloodConnector.WebAPI.App_Start
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto=>dto.UserId, opt=>opt.MapFrom(u=>u.UserId.ToString().Encrypt()))
                .ForMember(dto => dto.BloodGroup, opt => opt.MapFrom(u => u.BloodGroup.Symbole))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(u => u.Country.Name))
                .ForMember(dto => dto.Attachments, opt=>opt.MapFrom(u=>u.Attachments));
        }
    }
}
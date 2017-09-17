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
                .ForMember(ud => ud.FirstName, opt => opt.MapFrom(u => u.FirstName))
                .ForMember(ud => ud.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(ud => ud.ContactNumber, opt => opt.MapFrom(u => u.PhoneNumber))
                .ForMember(ud => ud.BloodGroup, opt => opt.MapFrom(u => u.BloodGroup.Symbole));
        }
    }
}
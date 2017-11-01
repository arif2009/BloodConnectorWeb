using AutoMapper;
using BloodConnector.WebAPI.Helper;
using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.VM;

namespace BloodConnector.WebAPI.App_Start
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserVM>()
                .ForMember(dto=>dto.UserId, opt=>opt.MapFrom(u=>u.UserId.ToString().Encrypt()))
                .ForMember(dto => dto.BloodGroup, opt => opt.MapFrom(u => u.BloodGroup.Symbole))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(u => u.Country.Name))
                .ForMember(dto => dto.Attachments, opt=>opt.MapFrom(u=>u.Attachments));
        }
    }
}
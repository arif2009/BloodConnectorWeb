using System.Linq;
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
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(u => u.UserId.ToString().Encrypt()))
                .ForMember(dto => dto.BloodGroup, opt => opt.MapFrom(u => u.BloodGroup.Symbole))
                .ForMember(dto => dto.BloodGroupName, opt => opt.MapFrom(u => u.BloodGroup.GroupName))
                .ForMember(dto => dto.SimilarBlood, opt => opt.MapFrom(u=>u.BloodGroup.Users.Count))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(u => u.Country.Name))
                .ForMember(dto => dto.Attachments, opt => opt.MapFrom(u => u.Attachments));

            CreateMap<Attachment, AttachmentVM>();

            CreateMap<User, DeveloperVM>()
                .ForMember(vm => vm.Name, u => u.MapFrom(x => ProjectHelper.GetFullName(x.FirstName, x.LastName, x.NikeName)))
                .ForMember(vm=>vm.Email, u=>u.MapFrom(x=>x.Email))
                .ForMember(vm=>vm.MobileNo, u=>u.MapFrom(x=>x.PhoneNumber))
                .ForMember(vm=>vm.ProfileImage, u=>u.MapFrom(x=>x.Attachments.FirstOrDefault().FileName));
        }
    }
}
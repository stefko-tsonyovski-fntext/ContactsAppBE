using AutoMapper;
using Core.Dtos.Contact;
using Core.Dtos.User;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            this.CreateMap<Contact, ContactDto>()
                .ForMember(s => s.ContactType, opt => opt.MapFrom(c => c.ContactType.ToString().ToUpper()));

            this.CreateMap<CreateContactDto, Contact>()
                        .ForPath(s => s.ProfileImageUri, opt => opt.Ignore())
                        .ForPath(s => s.ContactType, opt => opt.Ignore());

            this.CreateMap<EditContactDto, Contact>()
                        .IgnoreAllPropertiesWithAnInaccessibleSetter()
                        .ForPath(s => s.Id, opt => opt.Ignore())
                        .ForPath(s => s.ProfileImageUri, opt => opt.Ignore())
                        .ForPath(s => s.ContactType, opt => opt.Ignore())
                        .ReverseMap();

            this.CreateMap<ApplicationUser, UserDto>();
        }
    }
}

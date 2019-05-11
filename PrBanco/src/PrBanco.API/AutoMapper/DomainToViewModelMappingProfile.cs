using AutoMapper;
using PrBanco.API.ViewModels;
using PrBanco.Domain.Entities;

namespace PrBanco.API.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Person, PersonViewModel>()
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
                 .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.LastName))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone.Number))
                 .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email.Address))
                 ;
        }

    }
}

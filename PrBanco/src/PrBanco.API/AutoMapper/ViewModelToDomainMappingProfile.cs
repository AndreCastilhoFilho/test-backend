using AutoMapper;
using PrBanco.API.ViewModels;
using PrBanco.Domain.Entities;
using PrBanco.Domain.ValueObjects;

namespace PrBanco.API.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PersonViewModel, Person>()
                .ConstructUsing(c => new Person(
                    c.Id,
                    new Name(c.FirstName, c.LastName),
                    new Email(c.Email),
                    new Phone(c.Phone),
                    Mapper.Map<Address>(c.Address)));
        }
    }
}

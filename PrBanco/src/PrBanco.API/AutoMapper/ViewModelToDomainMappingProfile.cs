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
                    new Email(c.EmailAddress),
                    new Phone(c.PhoneNumber),
                    new Address(c.Address?.Street, c.Address?.Number, c.Address?.Neighborhood, c.Address?.City, c.Address?.State, c.Address?.Country, c.Address?.ZipCode)));
        }
    }
}

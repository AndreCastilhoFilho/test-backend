using Flunt.Br.Validation;
using Flunt.Validations;

namespace PrBanco.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address()
        {

        }

        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            if( !string.IsNullOrEmpty( ZipCode) || !string.IsNullOrEmpty(street) )
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Street, 3, "Address.Street", "A rua deve conter pelo menos 3 caracteres")
                .IsCep(zipCode, "Address.ZipCode", "Cep inválido")
            );

        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}

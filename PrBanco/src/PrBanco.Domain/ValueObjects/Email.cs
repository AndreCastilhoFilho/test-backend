using Flunt.Validations;

namespace PrBanco.Domain.ValueObjects
{
    public class Email : ValueObject
    {

        public Email(string address)
        {
            Address = address;

            if (!string.IsNullOrEmpty(address))
                AddNotifications(new Contract()
                    .Requires()
                    .IsEmail(Address, "Email.Address", "E-mail inválido")
                );
        }

        public string Address { get; private set; }
    }
}

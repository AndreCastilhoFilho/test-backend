using Flunt.Br.Validation;
using Flunt.Validations;

namespace PrBanco.Domain.ValueObjects
{
    public class Phone : ValueObject
    {
        public Phone()
        {

        }

        public Phone(string number)
        {
            Number = number;

            AddNotifications(new Contract()
                .Requires()
                .IsPhone(number, "Phone.Address", "Telefone inválido")
            );
        }

        public string Number { private set; get; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace PrBanco.API.ViewModels
{
    public class PersonViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Primeiro Nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Phone(ErrorMessage = "Telefone Inválido")]
        public string PhoneNumber { get; set; }

        public AddressViewModel Address { get; set; }

    }

    public class AddressViewModel
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}

using PrBanco.Domain.ValueObjects;
using System;

namespace PrBanco.Domain.Entities
{
    public class Person : Entity
    {      
        public Person(Guid id, Name name, Email email, Phone phone, Address address)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;

            AddNotifications(name, email, phone, address);
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public Address Address { get; private set; }
    }
}

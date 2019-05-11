using PrBanco.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrBanco.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task Add(Person person);
        Task Update(Person person);
        Task Delete(Guid id);
        Task<IEnumerable<Person>> Get();
        Task<Person> GetById(Guid id);
        Task<Person> GetByEmail(string email);
    }
}

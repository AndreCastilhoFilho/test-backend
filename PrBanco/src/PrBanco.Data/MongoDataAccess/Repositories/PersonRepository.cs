using MongoDB.Driver;
using PrBanco.Data.MongoDataAccess.Context;
using PrBanco.Domain.Entities;
using PrBanco.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrBanco.Data.MongoDataAccess.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PrBancoDbContext _mongoContext;

        public PersonRepository(PrBancoDbContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task Add(Person person)
        {
            await _mongoContext.Persons.InsertOneAsync(person);
        }

        public async Task Delete(Guid id)
        {
            await _mongoContext.Persons.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> Get()
        {
            return await _mongoContext.Persons.Find(_ => true).ToListAsync();
        }

        public async Task<Person> GetByEmail(string email)
        {
            return await _mongoContext.Persons.Find(p => p.Email.Address == email).FirstOrDefaultAsync();
        }

        public async Task<Person> GetById(Guid id)
        {
            return await _mongoContext.Persons.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(Person person)
        {
            await _mongoContext.Persons.ReplaceOneAsync(p => p.Id == person.Id, person);
        }
    }
}

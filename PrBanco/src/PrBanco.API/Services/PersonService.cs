using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrBanco.API.ViewModels;
using PrBanco.Domain.Entities;
using PrBanco.Domain.Repositories;

namespace PrBanco.API.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public PersonService(IMapper mapper, IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<PersonViewModel>>(await _personRepository.Get());
        }

        public async Task<PersonViewModel> GetById(Guid id)
        {
            return _mapper.Map<PersonViewModel>(await _personRepository.GetById(id));
        }

        public async Task<ServiceResult> Register(PersonViewModel personViewModel)
        {
            var person = _mapper.Map<Person>(personViewModel);

            if (person.Invalid)
                return new ServiceResult(false, person.Notifications.Select(n => n.Message).ToList());

            await _personRepository.Add(person);

            return new ServiceResult() { Success = true };
        }

        public async Task Remove(Guid id)
        {
            await _personRepository.Delete(id);
        }

        public async Task<ServiceResult> Update(PersonViewModel personViewModel)
        {
            var person = _mapper.Map<Person>(personViewModel);
            var result = new ServiceResult() { Success = person.Valid };

            Person existingPerson = await _personRepository.GetByEmail(personViewModel.EmailAddress);

            if (existingPerson != null && existingPerson.Id != personViewModel.Id)
            {
                result.Success = false;
                result.Notifications.Add("E-mail já está em uso");
            }

            result.Notifications.AddRange(person.Notifications.Select(n => n.Message).ToList());

            if (person.Valid && result.Success)
                await _personRepository.Update(person);

            return result;
        }
    }
}

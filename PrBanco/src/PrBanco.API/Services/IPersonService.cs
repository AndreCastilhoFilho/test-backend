using PrBanco.API.ViewModels;
using System;
using System.Collections.Generic;

namespace PrBanco.API.Services
{
    public interface IPersonService
    {
        IEnumerable<PersonViewModel> GetAll();

        PersonViewModel GetById(Guid id);

        ServiceResult Register(PersonViewModel person);

        ServiceResult Update(PersonViewModel person);

        void Remove(Guid id);

    }
}

using PrBanco.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrBanco.API.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonViewModel>> GetAll();

        Task<PersonViewModel> GetById(Guid id);

        Task<ServiceResult> Register(PersonViewModel person);

        Task<ServiceResult> Update(PersonViewModel person);

        Task Remove(Guid id);

    }
}

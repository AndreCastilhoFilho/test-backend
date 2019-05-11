using Microsoft.AspNetCore.Mvc;
using PrBanco.API.Services;
using PrBanco.API.ViewModels;
using System;
using System.Linq;

namespace PrBanco.API.Controllers
{
    public class PersonController : ControllerBase
    {
        public readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Route("person-management")]
        public IActionResult Get()
        {
            return Ok(_personService.GetAll());
        }

        [HttpGet]
        [Route("person-management/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var personViewModel = _personService.GetById(id);

            if (personViewModel == null)
                return BadRequest(new
                {
                    success = false,
                    errors = "Person not found"
                });

            return Ok(personViewModel);
        }

        [HttpPost]
        [Route("person-management")]
        public IActionResult Post([FromBody]PersonViewModel personViewModel)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new
                {
                    success = false,
                    data = erros
                });
            }

            var result = _personService.Register(personViewModel);

            return Response(result);

        }

        [HttpPut]
        [Route("person-management")]
        public IActionResult Put([FromBody]PersonViewModel personViewModel)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new
                {
                    success = false,
                    data = erros
                });
            }

            var result = _personService.Update(personViewModel);

            return Response(result);
        }
       
        [HttpDelete]
        [Route("person-management")]
        public IActionResult Delete(Guid id)
        {
            _personService.Remove(id);

            return Ok();
        }

        protected new IActionResult Response(ServiceResult result)
        {
            if (result.Notifications.Any())
            {
                return BadRequest(new
                {
                    success = false,
                    data = result.Notifications
                });
            }

            return Ok(new { success = true });
        }


    }
}
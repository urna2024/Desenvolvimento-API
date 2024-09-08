using MapeiaVoto.Application.Models;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Domain.Interfaces;
using MapeiaVoto.Service.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MapeiaVoto.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidoPoliticoController : ControllerBase
    {
        private IBaseService<PartidoPolitico> _baseService;
        public PartidoPoliticoController(IBaseService<PartidoPolitico> baseService)
        {
            _baseService = baseService;
        }
        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Create(PartidoPoliticoModel partidopolitico)
        {
            if (partidopolitico == null)
                return NotFound();
            return Execute(() => _baseService.Add<PartidoPoliticoModel,
           PartidoPoliticoValidator>(partidopolitico));
        }

        [HttpPut]
        public IActionResult Update(PartidoPoliticoModel partidopolitico)
        {
            if (partidopolitico == null)
                return NotFound();
            return Execute(() => _baseService.Update<PartidoPoliticoModel,
           PartidoPoliticoValidator>(partidopolitico));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();
            Execute(() =>
            {
                _baseService.Delete(id);
                return true;
            });
            return new NoContentResult();
        }



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseService.GetById<PartidoPoliticoModel>(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Execute(() => _baseService.Get<CargoDisputadoModel>());
        }

    }
}


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
    public class GeneroController : ControllerBase
    {
        private IBaseService<Genero> _baseService;
        public GeneroController(IBaseService<Genero> baseService)
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
        public IActionResult Create(GeneroModel genero)
        {
            if (genero == null)
                return NotFound();
            return Execute(() => _baseService.Add<GeneroModel,
           GeneroValidator>(genero));
        }

        [HttpPut]
        public IActionResult Update(GeneroModel genero)
        {
            if (genero == null)
                return NotFound();
            return Execute(() => _baseService.Update<GeneroModel,
           GeneroValidator>(genero));
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

            return Execute(() => _baseService.GetById<GeneroModel>(id));
        }
    }
}

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
    public class NivelEscolaridadeController : ControllerBase
    {
        private IBaseService<NivelEscolaridade> _baseService;
        public NivelEscolaridadeController(IBaseService<NivelEscolaridade> baseService)
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
        public IActionResult Create(NivelEscolaridadeModel nivelescolaridade)
        {
            if (nivelescolaridade == null)
                return NotFound();
            return Execute(() => _baseService.Add<NivelEscolaridadeModel,
           NivelEscolaridadeValidator>(nivelescolaridade));
        }

        [HttpPut]
        public IActionResult Update(NivelEscolaridadeModel nivelescolaridade)
        {
            if (nivelescolaridade == null)
                return NotFound();
            return Execute(() => _baseService.Update<NivelEscolaridadeModel,
           NivelEscolaridadeValidator>(nivelescolaridade));
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

            return Execute(() => _baseService.GetById<NivelEscolaridadeModel>(id));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Execute(() => _baseService.Get<NivelEscolaridadeModel>());
        }
    }
}


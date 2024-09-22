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
    public class RendaFamiliarController : ControllerBase
    {
        private IBaseService<RendaFamiliar> _baseService;
        public RendaFamiliarController(IBaseService<RendaFamiliar> baseService)
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
        public IActionResult Create(RendaFamiliarModel rendafamiliar)
        {
            if (rendafamiliar == null)
                return NotFound();
            return Execute(() => _baseService.Add<RendaFamiliarModel,
           RendaFamiliarValidator>(rendafamiliar));
        }

        [HttpPut]
        public IActionResult Update(RendaFamiliarModel rendafamiliar)
        {
            if (rendafamiliar == null)
                return NotFound();
            return Execute(() => _baseService.Update<RendaFamiliarModel,
           RendaFamiliarValidator>(rendafamiliar));
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

            return Execute(() => _baseService.GetById<RendaFamiliarModel>(id));
        }
    }
}

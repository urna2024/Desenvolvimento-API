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
    public class PerfilUsuarioController : ControllerBase
    {
        private IBaseService<PerfilUsuario> _baseService;
        public PerfilUsuarioController(IBaseService<PerfilUsuario> baseService)
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
        public IActionResult Create(PerfilUsuarioModel perfilusuario)
        {
            if (perfilusuario == null)
                return NotFound();
            return Execute(() => _baseService.Add<PerfilUsuarioModel,
           PerfilUsuarioValidator>(perfilusuario));
        }

        [HttpPut]
        public IActionResult Update(PerfilUsuarioModel perfilusuario)
        {
            if (perfilusuario == null)
                return NotFound();
            return Execute(() => _baseService.Update<PerfilUsuarioModel,
           PerfilUsuarioValidator>(perfilusuario));
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

            return Execute(() => _baseService.GetById<PerfilUsuarioModel>(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Execute(() => _baseService.Get<PerfilUsuarioModel>());
        }
    }
}

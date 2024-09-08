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
    public class StatusController : ControllerBase
    {
        private IBaseService<Status> _baseService;
        public StatusController(IBaseService<Status> baseService)
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
        public IActionResult Create(StatusModel status)
        {
            if (status == null)
                return NotFound();
            return Execute(() => _baseService.Add<StatusModel,
           StatusValidator>(status));
        }

        [HttpPut]
        public IActionResult Update(StatusModel status)
        {
            if (status == null)
                return NotFound();
            return Execute(() => _baseService.Update<StatusModel,
           StatusValidator>(status));
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

            return Execute(() => _baseService.GetById<StatusModel>(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Execute(() => _baseService.Get<CargoDisputadoModel>());
        }


    }
}

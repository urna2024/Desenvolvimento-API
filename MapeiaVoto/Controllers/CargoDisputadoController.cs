using MapeiaVoto.Application.Models;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Domain.Interfaces;
using MapeiaVoto.Service.Validators;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MapeiaVoto.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDisputadoController : ControllerBase
    {
        private readonly IBaseService<CargoDisputado> _baseService;

        public CargoDisputadoController(IBaseService<CargoDisputado> baseService)
        {
            _baseService = baseService;
        }

        // Método privado para executar operações
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

        // Criar um novo CargoDisputado
        [HttpPost]
        public IActionResult Create(CargoDisputadoModel cargoDisputado)
        {
            if (cargoDisputado == null)
                return NotFound();

            return Execute(() => _baseService.Add<CargoDisputadoModel, CargoDisputadoValidator>(cargoDisputado));
        }

        // Atualizar um CargoDisputado existente
        [HttpPut]
        public IActionResult Update(CargoDisputadoModel cargoDisputado)
        {
            if (cargoDisputado == null)
                return NotFound();

            return Execute(() => _baseService.Update<CargoDisputadoModel, CargoDisputadoValidator>(cargoDisputado));
        }

        // Deletar um CargoDisputado por ID
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

        // Obter um CargoDisputado por ID
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseService.GetById<CargoDisputadoModel>(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Execute(() => _baseService.Get<CargoDisputadoModel>());
        }


    }
}

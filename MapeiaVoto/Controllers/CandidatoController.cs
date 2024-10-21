using MapeiaVoto.Application.Dto;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Domain.Interfaces;
using MapeiaVoto.Infrastructure.Data.Context;
using MapeiaVoto.Service.Validators; // Adicionado
using FluentValidation; // Adicionado
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapeiaVoto.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly IBaseService<Candidato> _baseService;
        private readonly SqlServerContext _context;
        private readonly CandidatoValidator _candidatoValidator; // Adicionado Validator

        public CandidatoController(IBaseService<Candidato> baseService, SqlServerContext context)
        {
            _baseService = baseService;
            _context = context;
            _candidatoValidator = new CandidatoValidator(); // Instanciando o Validator
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Candidato>> Create(CandidadoDto request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Mapeia o DTO para a entidade Candidato
                    var candidato = new Candidato
                    {
                        nomeCompleto = request.nomeCompleto,
                        nomeUrna = request.nomeUrna,
                        dataNascimento = request.dataNascimento.Date,
                        uf = request.uf,
                        municipio = request.municipio,
                        foto = request.foto,
                        idStatus = request.idStatus,
                        idPartidoPolitico = request.idPartidoPolitico,
                        idCargoDisputado = request.idCargoDisputado
                    };

                    // Validação da entidade Candidato
                    var validationResult = _candidatoValidator.Validate(candidato);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(validationResult.Errors);
                    }

                    _context.candidato.Add(candidato);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var createdCandidato = await _context.candidato
                        .Include(c => c.status)
                        .Include(c => c.partidopolitico)
                        .Include(c => c.cargodisputado)
                        .FirstOrDefaultAsync(c => c.id == candidato.id);

                    return CreatedAtAction(nameof(Create), new { id = createdCandidato.id }, createdCandidato);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar candidato: {ex.Message}");
                }
            }
        }



        [HttpGet("dadosBasicos")]
        public async Task<ActionResult<List<object>>> GetBasicCandidatoData()
        {
            try
            {
                var candidatos = await _context.candidato
                    .Include(c => c.status) // Inclui o relacionamento com Status
                    .Select(c => new
                    {
                        c.id,
                        c.nomeCompleto,
                        c.nomeUrna,
                        c.uf,
                        c.municipio,
                        c.dataNascimento,
                        c.idStatus, // Inclui o idStatus
                        statusNome = c.status.nome, // Inclui o nome do status
                    })
                    .ToListAsync();

                return Ok(candidatos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar os dados básicos dos candidatos.");
            }
        }

        [HttpGet("{id}/dadosCompletos")]
        public async Task<ActionResult<Candidato>> GetCompleteCandidatoData(long id)
        {
            try
            {
                var candidato = await _context.candidato
                    .Include(c => c.partidopolitico) // Inclui o relacionamento com PartidoPolítico
                    .Include(c => c.cargodisputado)  // Inclui o relacionamento com CargoDisputado
                    .Include(c => c.status)          // Inclui o relacionamento com Status
                    .FirstOrDefaultAsync(c => c.id == id);

                if (candidato == null)
                {
                    return NotFound("Candidato não encontrado.");
                }

                return Ok(candidato);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar os dados completos do candidato.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CandidadoDto request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Verificar se o PartidoPolitico existe
                    var partidoExistente = await _context.partidopolitico.FirstOrDefaultAsync(pp => pp.id == request.idPartidoPolitico);
                    if (partidoExistente == null)
                    {
                        return BadRequest($"Partido Político com id {request.idPartidoPolitico} não encontrado.");
                    }

                    // Verificar se o CargoDisputado existe
                    var cargoExistente = await _context.cargodisputado.FirstOrDefaultAsync(cd => cd.id == request.idCargoDisputado);
                    if (cargoExistente == null)
                    {
                        return BadRequest($"Cargo Disputado com id {request.idCargoDisputado} não encontrado.");
                    }

                    // Verificar se o Status existe
                    var statusExistente = await _context.status.FirstOrDefaultAsync(s => s.id == request.idStatus);
                    if (statusExistente == null)
                    {
                        return BadRequest($"Status com id {request.idStatus} não encontrado.");
                    }

                    // Buscar candidato existente no banco de dados
                    var candidatoExistente = await _context.candidato.FirstOrDefaultAsync(c => c.id == id);
                    if (candidatoExistente == null)
                    {
                        return NotFound("Candidato não encontrado.");
                    }

                    // Atualizar os dados do candidato existente com os novos valores
                    candidatoExistente.nomeCompleto = request.nomeCompleto;
                    candidatoExistente.nomeUrna = request.nomeUrna;
                    candidatoExistente.dataNascimento = request.dataNascimento.Date;
                    candidatoExistente.uf = request.uf;
                    candidatoExistente.municipio = request.municipio;
                    candidatoExistente.foto = request.foto;
                    candidatoExistente.idStatus = request.idStatus;
                    candidatoExistente.idPartidoPolitico = request.idPartidoPolitico;
                    candidatoExistente.idCargoDisputado = request.idCargoDisputado;

                    // Validação do objeto candidato atualizado
                    var validationResult = _candidatoValidator.Validate(candidatoExistente);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(validationResult.Errors);
                    }

                    // Atualizar o candidato no banco de dados
                    _context.candidato.Update(candidatoExistente);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(candidatoExistente);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    // Captura a mensagem detalhada da exceção
                    var innerExceptionMessage = ex.InnerException?.Message ?? "Nenhuma exceção interna";
                    var fullExceptionMessage = $"Erro ao atualizar candidato: {ex.Message} | Exceção interna: {innerExceptionMessage}";

                    return StatusCode(StatusCodes.Status500InternalServerError, fullExceptionMessage);
                }
            }
        }




        // Deletar um candidato por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var candidatoExistente = await _context.candidato.FirstOrDefaultAsync(c => c.id == id);

                    if (candidatoExistente == null)
                    {
                        return NotFound("Candidato não encontrado.");
                    }

                    _context.candidato.Remove(candidatoExistente);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok("Candidato excluído com sucesso.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir candidato.");
                }
            }
        }

        // Obter todos os tipos de Status
        [HttpGet("tipoStatus")]
        public IActionResult ObterTiposStatus()
        {
            try
            {
                var tiposStatus = _context.status.ToList();
                return Ok(tiposStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Obter todos os tipos de Partido Político
        [HttpGet("tipoPartidoPolitico")]
        public IActionResult ObterTiposPartidoPolitico()
        {
            try
            {
                var tiposPartido = _context.partidopolitico.ToList();
                return Ok(tiposPartido);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Obter todos os tipos de Cargo Disputado
        [HttpGet("tipoCargoDisputado")]
        public IActionResult ObterTiposCargoDisputado()
        {
            try
            {
                var tiposCargo = _context.cargodisputado.ToList();
                return Ok(tiposCargo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Alterar o status de um candidato
        [HttpPatch("{id}/mudarStatus")]
        public async Task<IActionResult> MudarStatus(int id, [FromBody] int novoStatusId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var candidato = await _context.candidato.FirstOrDefaultAsync(c => c.id == id);
                    if (candidato == null)
                    {
                        return NotFound("Candidato não encontrado.");
                    }

                    var status = await _context.status.FirstOrDefaultAsync(s => s.id == novoStatusId);
                    if (status == null)
                    {
                        return NotFound("Status não encontrado.");
                    }

                    candidato.idStatus = novoStatusId;
                    _context.candidato.Update(candidato);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(candidato);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao mudar o status: {ex.Message}");
                }
            }
        }

        



    }
}

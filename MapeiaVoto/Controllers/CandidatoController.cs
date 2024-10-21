using MapeiaVoto.Application.Dto;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Domain.Interfaces;
using MapeiaVoto.Infrastructure.Data.Context;
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

        public CandidatoController(IBaseService<Candidato> baseService, SqlServerContext context)
        {
            _baseService = baseService;
            _context = context;
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

        // Criar um novo candidato
        [HttpPost]
        public async Task<ActionResult<Candidato>> Create(CandidadoDto request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Verificar se as chaves estrangeiras existem
                    if (!_context.status.Any(s => s.id == request.idStatus))
                    {
                        return BadRequest($"Status com id {request.idStatus} não encontrado.");
                    }
                    if (!_context.partidopolitico.Any(pp => pp.id == request.idPartidoPolitico))
                    {
                        return BadRequest($"Partido Político com id {request.idPartidoPolitico} não encontrado.");
                    }
                    if (!_context.cargodisputado.Any(cd => cd.id == request.idCargoDisputado))
                    {
                        return BadRequest($"Cargo Disputado com id {request.idCargoDisputado} não encontrado.");
                    }

                    var novoCandidato = new Candidato
                    {
                        id = request.id,
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

                    _context.candidato.Add(novoCandidato);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var createdCandidato = await _context.candidato
                        .Include(c => c.status)
                        .Include(c => c.partidopolitico)
                        .Include(c => c.cargodisputado)
                        .FirstOrDefaultAsync(c => c.id == novoCandidato.id);

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


        // Atualizar um candidato existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CandidadoDto request)
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

                    candidato.nomeCompleto = request.nomeCompleto;
                    candidato.nomeUrna = request.nomeUrna;
                    candidato.dataNascimento = request.dataNascimento.Date;
                    candidato.uf = request.uf;
                    candidato.municipio = request.municipio;
                    candidato.foto = request.foto;
                    candidato.idStatus = request.idStatus;
                    candidato.idPartidoPolitico = request.idPartidoPolitico;
                    candidato.idCargoDisputado = request.idCargoDisputado;

                    _context.candidato.Update(candidato);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(candidato);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar candidato.");
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

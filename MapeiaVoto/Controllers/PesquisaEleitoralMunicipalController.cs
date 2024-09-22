using MapeiaVoto.Application.Dto;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SGFME.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesquisaEleitoralController : ControllerBase
    {
        private readonly SqlServerContext _context;

        public PesquisaEleitoralController(SqlServerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PesquisaEleitoralDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Verificar se o usuário existe
                    var usuarioEntrevistador = await _context.usuario.FirstOrDefaultAsync(u => u.id == request.idUsuario);
                    if (usuarioEntrevistador == null)
                    {
                        return BadRequest($"Usuário entrevistador com ID {request.idUsuario} não encontrado.");
                    }

                    // Verificar se o status existe
                    var statusPesquisa = await _context.status.FirstOrDefaultAsync(s => s.id == request.idStatus);
                    if (statusPesquisa == null)
                    {
                        return BadRequest($"Status com ID {request.idStatus} não encontrado.");
                    }

                    // Verificar se os candidatos existem
                    Candidato candidatoPrefeito = null;
                    Candidato candidatoVereador = null;

                    if (request.idCandidatoPrefeito.HasValue)
                    {
                        candidatoPrefeito = await _context.candidato.FirstOrDefaultAsync(c => c.id == request.idCandidatoPrefeito);
                        if (candidatoPrefeito == null)
                        {
                            return BadRequest($"Candidato a prefeito com ID {request.idCandidatoPrefeito} não encontrado.");
                        }
                    }

                    if (request.idCandidatoVereador.HasValue)
                    {
                        candidatoVereador = await _context.candidato.FirstOrDefaultAsync(c => c.id == request.idCandidatoVereador);
                        if (candidatoVereador == null)
                        {
                            return BadRequest($"Candidato a vereador com ID {request.idCandidatoVereador} não encontrado.");
                        }
                    }

                    // Verificar se há apenas um entrevistado na pesquisa
                    if (request.entrevistado.Count != 1)
                    {
                        return BadRequest("A pesquisa eleitoral deve ter exatamente um entrevistado.");
                    }

                    // Criar o novo entrevistado
                    var entrevistadoDto = request.entrevistado.First();
                    var novoEntrevistado = new Entrevistado
                    {
                        nomeCompleto = entrevistadoDto.nomeCompleto,
                        dataNascimento = entrevistadoDto.dataNascimento,
                        uf = entrevistadoDto.uf,
                        municipio = entrevistadoDto.municipio,
                        celular = entrevistadoDto.celular,
                        idGenero = entrevistadoDto.idGenero,
                        idNivelEscolaridade = entrevistadoDto.idNivelEscolaridade,
                        idRendaFamiliar = entrevistadoDto.idRendaFamiliar
                    };

                    // Criar a nova entidade PesquisaEleitoralMunicipal
                    var novaPesquisa = new PesquisaEleitoralMunicipal
                    {
                        dataEntrevista = request.dataEntrevista,
                        uf = request.uf,
                        municipio = request.municipio,
                        votoIndeciso = request.votoIndeciso,
                        votoBrancoNulo = request.votoBrancoNulo,
                        sugestaoMelhoria = request.sugestaoMelhoria,
                        idCandidatoPrefeito = request.idCandidatoPrefeito,
                        idCandidatoVereador = request.idCandidatoVereador,
                        idUsuario = request.idUsuario, // Adicionando id do entrevistador
                        idStatus = request.idStatus,  // Adicionando id do status
                        entrevistado = new List<Entrevistado>() // Inicializar a coleção de entrevistados
                    };

                    // Adicionar o novo entrevistado à coleção
                    novaPesquisa.entrevistado.Add(novoEntrevistado);

                    // Adicionar o novo entrevistado e a pesquisa no contexto
                    _context.entrevistado.Add(novoEntrevistado);
                    _context.pesquisaeleitoralmunicipal.Add(novaPesquisa);
                    await _context.SaveChangesAsync();

                    // Confirmar a transação
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(Create), new { id = novaPesquisa.id }, novaPesquisa);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"Erro ao criar a pesquisa eleitoral: {ex.Message}");
                }
            }
        }
    }
}

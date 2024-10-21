using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapeiaVoto.Application.Dto;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapeiaVoto.Domain.Interfaces;
using MapeiaVoto.Service.Services;
using MapeiaVoto.Application.Models;

namespace MapeiaVoto.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesquisaEleitoralController : ControllerBase
    {
        private readonly SqlServerContext _context;
        private IBaseService<PesquisaEleitoralMunicipal> _baseService;

        public PesquisaEleitoralController(SqlServerContext context, IBaseService<PesquisaEleitoralMunicipal> baseService)
        {
            _context = context;
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





        // Criar uma nova pesquisa eleitoral
        [HttpPost]
        public async Task<ActionResult<PesquisaEleitoralMunicipal>> Create(PesquisaEleitoralMunicipalDto request)
        {
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

                    // Verificar se os candidatos existem e carregar todos os dados
                    CandidadoDto candidatoPrefeitoDto = null;
                    CandidadoDto candidatoVereadorDto = null;

                    if (request.idCandidatoPrefeito.HasValue)
                    {
                        var candidatoPrefeito = await _context.candidato.FirstOrDefaultAsync(c => c.id == request.idCandidatoPrefeito);
                        if (candidatoPrefeito == null)
                        {
                            return BadRequest($"Candidato a prefeito com ID {request.idCandidatoPrefeito} não encontrado.");
                        }
                        candidatoPrefeitoDto = new CandidadoDto
                        {
                            id = candidatoPrefeito.id,
                            nomeCompleto = candidatoPrefeito.nomeCompleto,
                            nomeUrna = candidatoPrefeito.nomeUrna,
                            dataNascimento = candidatoPrefeito.dataNascimento,
                            uf = candidatoPrefeito.uf,
                            municipio = candidatoPrefeito.municipio,
                            foto = candidatoPrefeito.foto,
                            idStatus = candidatoPrefeito.idStatus,
                            idPartidoPolitico = candidatoPrefeito.idPartidoPolitico,
                            idCargoDisputado = candidatoPrefeito.idCargoDisputado
                        };
                    }

                    if (request.idCandidatoVereador.HasValue)
                    {
                        var candidatoVereador = await _context.candidato.FirstOrDefaultAsync(c => c.id == request.idCandidatoVereador);
                        if (candidatoVereador == null)
                        {
                            return BadRequest($"Candidato a vereador com ID {request.idCandidatoVereador} não encontrado.");
                        }
                        candidatoVereadorDto = new CandidadoDto
                        {
                            id = candidatoVereador.id,
                            nomeCompleto = candidatoVereador.nomeCompleto,
                            nomeUrna = candidatoVereador.nomeUrna,
                            dataNascimento = candidatoVereador.dataNascimento,
                            uf = candidatoVereador.uf,
                            municipio = candidatoVereador.municipio,
                            foto = candidatoVereador.foto,
                            idStatus = candidatoVereador.idStatus,
                            idPartidoPolitico = candidatoVereador.idPartidoPolitico,
                            idCargoDisputado = candidatoVereador.idCargoDisputado
                        };
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
                        idUsuario = request.idUsuario,
                        idStatus = request.idStatus,
                        entrevistado = new List<Entrevistado> { novoEntrevistado }
                    };

                    // Adicionar o novo entrevistado e a pesquisa no contexto
                    _context.entrevistado.Add(novoEntrevistado);
                    _context.pesquisaeleitoralmunicipal.Add(novaPesquisa);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    // Retornar a pesquisa com todos os dados completos dos candidatos
                    var response = new
                    {
                        novaPesquisa,
                        CandidatoPrefeito = candidatoPrefeitoDto,
                        CandidatoVereador = candidatoVereadorDto
                    };

                    return CreatedAtAction(nameof(Create), new { id = novaPesquisa.id }, response);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar a pesquisa eleitoral: {ex.Message}");
                }
            }
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<PesquisaEleitoralMunicipal>>> GetAllPesquisasEleitorais()
        {
            try
            {
                var pesquisas = await _context.pesquisaeleitoralmunicipal
                    .Include(p => p.entrevistado) // Inclui os entrevistados na pesquisa
                    .Include(p => p.status) // Inclui o status da pesquisa
                    .Include(p => p.candidatoPrefeito) // Inclui o candidato a prefeito, se houver
                    .Include(p => p.candidatoVereador) // Inclui o candidato a vereador, se houver
                    .ToListAsync();

                if (pesquisas == null || pesquisas.Count == 0)
                {
                    return NotFound("Nenhuma pesquisa eleitoral encontrada.");
                }

                return Ok(pesquisas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar as pesquisas eleitorais: {ex.Message}");
            }
        }


        // Buscar dados básicos de todas as pesquisas
        [HttpGet("dadosBasicos")]
        public async Task<ActionResult<List<object>>> GetBasicPesquisaEleitoralData()
        {
            try
            {
                var pesquisas = await _context.pesquisaeleitoralmunicipal
                    .Include(p => p.status)
                    .Include(p => p.entrevistado)
                    .Select(p => new
                    {
                        p.id,
                        p.dataEntrevista,
                        p.uf,
                        p.municipio,
                        p.idStatus,
                        statusNome = p.status.nome,
                        entrevistado = p.entrevistado.Select(e => e.nomeCompleto).FirstOrDefault() // Pega o nome do primeiro entrevistado
                    })
                    .ToListAsync();

                return Ok(pesquisas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar os dados básicos das pesquisas eleitorais: {ex.Message}");
            }
        }


        // Buscar uma pesquisa por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPesquisaById(int id)
        {
            try
            {
                var pesquisa = await _context.pesquisaeleitoralmunicipal
                    .Include(p => p.entrevistado)
                    .Include(p => p.status)
                    .Include(p => p.candidatoPrefeito)
                    .Include(p => p.candidatoVereador)
                    .FirstOrDefaultAsync(p => p.id == id);

                if (pesquisa == null)
                {
                    return NotFound("Pesquisa eleitoral não encontrada.");
                }

                return Ok(pesquisa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar a pesquisa eleitoral: {ex.Message}");
            }
        }

        // Excluir uma pesquisa
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var pesquisaExistente = await _context.pesquisaeleitoralmunicipal
                        .Include(p => p.entrevistado)
                        .FirstOrDefaultAsync(p => p.id == id);

                    if (pesquisaExistente == null)
                    {
                        return NotFound("Pesquisa eleitoral não encontrada.");
                    }

                    _context.entrevistado.Remove(pesquisaExistente.entrevistado.FirstOrDefault()); // Ajuste para remover o primeiro entrevistado
                    _context.pesquisaeleitoralmunicipal.Remove(pesquisaExistente);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok("Pesquisa eleitoral excluída com sucesso.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir pesquisa eleitoral.");
                }
            }
        }

        // Atualizar o status de uma pesquisa
        [HttpPatch("{id}/mudarStatus")]
        public async Task<IActionResult> MudarStatus(int id, [FromBody] int novoStatusId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var pesquisa = await _context.pesquisaeleitoralmunicipal.FirstOrDefaultAsync(p => p.id == id);
                    if (pesquisa == null)
                    {
                        return NotFound("Pesquisa eleitoral não encontrada.");
                    }

                    var status = await _context.status.FirstOrDefaultAsync(s => s.id == novoStatusId);
                    if (status == null)
                    {
                        return NotFound("Status não encontrado.");
                    }

                    pesquisa.idStatus = novoStatusId;
                    _context.pesquisaeleitoralmunicipal.Update(pesquisa);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(pesquisa);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao mudar o status da pesquisa eleitoral.");
                }
            }
        }

        // Buscar todos os partidos políticos
        [HttpGet("partidos")]
        public IActionResult ObterPartidosPoliticos()
        {
            try
            {
                var partidos = _context.partidopolitico.ToList();
                return Ok(partidos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar partidos políticos: {ex.Message}");
            }
        }

        // Buscar todos os níveis de escolaridade
        [HttpGet("nivelEscolaridade")]
        public IActionResult ObterNiveisEscolaridade()
        {
            try
            {
                var niveisEscolaridade = _context.nivelescolaridade.ToList();
                return Ok(niveisEscolaridade);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar níveis de escolaridade: {ex.Message}");
            }
        }

        // Buscar todos os gêneros
        [HttpGet("generos")]
        public IActionResult ObterGeneros()
        {
            try
            {
                var generos = _context.genero.ToList();
                return Ok(generos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar gêneros: {ex.Message}");
            }
        }

        // Buscar todos os níveis de renda familiar
        [HttpGet("rendaFamiliar")]
        public IActionResult ObterRendaFamiliar()
        {
            try
            {
                var rendasFamiliares = _context.rendafamiliar.ToList();
                return Ok(rendasFamiliares);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar renda familiar: {ex.Message}");
            }
        }

        // Endpoint para dados completos de uma pesquisa eleitoral específica
        [HttpGet("{id}/dadosCompletos")]
        public async Task<ActionResult<object>> GetCompletePesquisaEleitoralData(int id)
        {
            try
            {
                var pesquisa = await _context.pesquisaeleitoralmunicipal
                    .Include(p => p.entrevistado)
                    .Include(p => p.status)
                    .Include(p => p.candidatoPrefeito)
                        .ThenInclude(c => c.partidopolitico) // Inclui o partido do candidato a prefeito
                    .Include(p => p.candidatoVereador)
                        .ThenInclude(c => c.partidopolitico) // Inclui o partido do candidato a vereador
                    .Select(p => new
                    {
                        p.id,
                        p.dataEntrevista,
                        p.uf,
                        p.municipio,
                        p.votoIndeciso,
                        p.votoBrancoNulo,
                        p.sugestaoMelhoria,
                        idStatus = p.status.id,
                        statusNome = p.status.nome,

                        // Dados do entrevistado
                        entrevistado = p.entrevistado.FirstOrDefault() != null ? new
                        {
                            p.entrevistado.FirstOrDefault().nomeCompleto,
                            p.entrevistado.FirstOrDefault().dataNascimento,
                            p.entrevistado.FirstOrDefault().uf,
                            p.entrevistado.FirstOrDefault().municipio,
                            p.entrevistado.FirstOrDefault().celular,
                            p.entrevistado.FirstOrDefault().idGenero,
                            p.entrevistado.FirstOrDefault().idNivelEscolaridade,
                            p.entrevistado.FirstOrDefault().idRendaFamiliar
                        } : null,

                        // Dados do candidato a prefeito
                        candidatoPrefeito = p.candidatoPrefeito != null ? new
                        {
                            p.candidatoPrefeito.nomeCompleto,
                            p.candidatoPrefeito.nomeUrna,
                            partido = p.candidatoPrefeito.partidopolitico.nome,
                            siglaPartido = p.candidatoPrefeito.partidopolitico.sigla
                        } : null,

                        // Dados do candidato a vereador
                        candidatoVereador = p.candidatoVereador != null ? new
                        {
                            p.candidatoVereador.nomeCompleto,
                            p.candidatoVereador.nomeUrna,
                            partido = p.candidatoVereador.partidopolitico.nome,
                            siglaPartido = p.candidatoVereador.partidopolitico.sigla
                        } : null
                    })
                    .FirstOrDefaultAsync(p => p.id == id);

                if (pesquisa == null)
                {
                    return NotFound("Pesquisa eleitoral não encontrada.");
                }

                return Ok(pesquisa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar os dados completos da pesquisa eleitoral: {ex.Message}");
            }
        }


        [HttpGet("resultados/prefeitos")]
        public async Task<ActionResult<object>> GetResultadosPrefeitos()
        {
            try
            {
                var totalVotos = await _context.pesquisaeleitoralmunicipal.CountAsync();

                if (totalVotos == 0)
                {
                    return NotFound("Nenhuma pesquisa eleitoral encontrada.");
                }

                var resultados = await _context.pesquisaeleitoralmunicipal
                    .Where(p => p.idCandidatoPrefeito != null && p.votoBrancoNulo == false && p.votoIndeciso == false)
                    .GroupBy(p => p.candidatoPrefeito.nomeCompleto)
                    .Select(g => new
                    {
                        CandidatoPrefeito = g.Key,
                        TotalVotos = g.Count(),
                        PercentualVotos = (double)g.Count() / totalVotos * 100
                    })
                    .ToListAsync();

                return Ok(new
                {
                    TotalEntrevistas = totalVotos,
                    ResultadosPrefeitos = resultados
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar os resultados das intenções de voto para prefeito: {ex.Message}");
            }
        }

        [HttpGet("resultados/vereadores")]
        public async Task<ActionResult<object>> GetResultadosVereadores()
        {
            try
            {
                var totalVotos = await _context.pesquisaeleitoralmunicipal.CountAsync();

                if (totalVotos == 0)
                {
                    return NotFound("Nenhuma pesquisa eleitoral encontrada.");
                }

                var resultados = await _context.pesquisaeleitoralmunicipal
                    .Where(p => p.idCandidatoVereador != null && p.votoBrancoNulo == false && p.votoIndeciso == false)
                    .GroupBy(p => p.candidatoVereador.nomeCompleto)
                    .Select(g => new
                    {
                        CandidatoVereador = g.Key,
                        TotalVotos = g.Count(),
                        PercentualVotos = (double)g.Count() / totalVotos * 100
                    })
                    .ToListAsync();

                return Ok(new
                {
                    TotalEntrevistas = totalVotos,
                    ResultadosVereadores = resultados
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar os resultados das intenções de voto para vereador: {ex.Message}");
            }
        }

        [HttpGet("resultados/indecisos-brancos-nulos")]
        public async Task<ActionResult<object>> GetVotosIndecisosBrancosNulos()
        {
            try
            {
                var totalVotos = await _context.pesquisaeleitoralmunicipal.CountAsync();

                if (totalVotos == 0)
                {
                    return NotFound("Nenhuma pesquisa eleitoral encontrada.");
                }

                var votosIndecisos = await _context.pesquisaeleitoralmunicipal
                    .CountAsync(p => p.votoIndeciso == true);
                var votosBrancosNulos = await _context.pesquisaeleitoralmunicipal
                    .CountAsync(p => p.votoBrancoNulo == true);

                return Ok(new
                {
                    TotalEntrevistas = totalVotos,
                    VotosIndecisos = votosIndecisos,
                    PercentualIndecisos = (double)votosIndecisos / totalVotos * 100,
                    VotosBrancosNulos = votosBrancosNulos,
                    PercentualBrancosNulos = (double)votosBrancosNulos / totalVotos * 100
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar os resultados dos votos indecisos, brancos e nulos: {ex.Message}");
            }
        }

        [HttpGet("resultados/renda-familiar")]
        public async Task<ActionResult<object>> GetResultadosPorRendaFamiliar()
        {
            try
            {
                var totalVotos = await _context.pesquisaeleitoralmunicipal.CountAsync();

                if (totalVotos == 0)
                {
                    return NotFound("Nenhuma pesquisa eleitoral encontrada.");
                }

                var resultados = await _context.pesquisaeleitoralmunicipal
                    .Include(p => p.entrevistado)
                    .Where(p => p.entrevistado.FirstOrDefault().idRendaFamiliar != null)
                    .GroupBy(p => p.entrevistado.FirstOrDefault().rendafamiliar.nome)
                    .Select(g => new
                    {
                        RendaFamiliar = g.Key,
                        TotalVotos = g.Count(),
                        PercentualVotos = (double)g.Count() / totalVotos * 100
                    })
                    .ToListAsync();

                return Ok(new
                {
                    TotalEntrevistas = totalVotos,
                    ResultadosPorRendaFamiliar = resultados
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar os resultados das intenções de voto por renda familiar: {ex.Message}");
            }
        }

        [HttpGet("resultados/genero")]
        public async Task<ActionResult<object>> GetResultadosPorGenero()
        {
            try
            {
                var totalVotos = await _context.pesquisaeleitoralmunicipal.CountAsync();

                if (totalVotos == 0)
                {
                    return NotFound("Nenhuma pesquisa eleitoral encontrada.");
                }

                var resultados = await _context.pesquisaeleitoralmunicipal
                    .Include(p => p.entrevistado)
                    .Where(p => p.entrevistado.FirstOrDefault().idGenero != null)
                    .GroupBy(p => p.entrevistado.FirstOrDefault().genero.nome)
                    .Select(g => new
                    {
                        Genero = g.Key,
                        TotalVotos = g.Count(),
                        PercentualVotos = (double)g.Count() / totalVotos * 100
                    })
                    .ToListAsync();

                return Ok(new
                {
                    TotalEntrevistas = totalVotos,
                    ResultadosPorGenero = resultados
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar os resultados das intenções de voto por gênero: {ex.Message}");
            }
        }


        [HttpGet("resultados/nivel-escolaridade")]
        public async Task<ActionResult<object>> GetResultadosPorNivelEscolaridade()
        {
            try
            {
                var totalVotos = await _context.pesquisaeleitoralmunicipal.CountAsync();

                if (totalVotos == 0)
                {
                    return NotFound("Nenhuma pesquisa eleitoral encontrada.");
                }

                var resultados = await _context.pesquisaeleitoralmunicipal
                    .Include(p => p.entrevistado)
                    .Where(p => p.entrevistado.FirstOrDefault().idNivelEscolaridade != null)
                    .GroupBy(p => p.entrevistado.FirstOrDefault().nivelescolaridade.nome)
                    .Select(g => new
                    {
                        NivelEscolaridade = g.Key,
                        TotalVotos = g.Count(),
                        PercentualVotos = (double)g.Count() / totalVotos * 100
                    })
                    .ToListAsync();

                return Ok(new
                {
                    TotalEntrevistas = totalVotos,
                    ResultadosPorNivelEscolaridade = resultados
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao gerar os resultados das intenções de voto por nível de escolaridade: {ex.Message}");
            }
        }

        // Endpoint para buscar candidatos a prefeito de acordo com UF e Município e incluir a sigla do partido
        [HttpGet("prefeitos")]
        public async Task<ActionResult<IEnumerable<object>>> GetPrefeitos([FromQuery] string uf, [FromQuery] string municipio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uf) || string.IsNullOrWhiteSpace(municipio))
                {
                    return BadRequest("UF e município são obrigatórios.");
                }

                var prefeitos = await _context.candidato
                    .Include(c => c.partidopolitico) // Inclui o partido político relacionado
                    .Where(c => c.cargodisputado.nome == "Prefeito" && c.uf == uf && c.municipio == municipio)
                    .Select(c => new
                    {
                        c.nomeCompleto,
                        c.nomeUrna,
                        Partido = c.partidopolitico.nome,
                        SiglaPartido = c.partidopolitico.sigla
                    })
                    .ToListAsync();

                if (!prefeitos.Any())
                {
                    return NotFound("Nenhum candidato a prefeito encontrado para o UF e município especificados.");
                }

                return Ok(prefeitos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar candidatos a prefeito: {ex.Message}");
            }
        }


        // Endpoint para buscar candidatos a vereador de acordo com UF e Município e incluir a sigla do partido
        [HttpGet("vereadores")]
        public async Task<ActionResult<IEnumerable<object>>> GetVereadores([FromQuery] string uf, [FromQuery] string municipio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uf) || string.IsNullOrWhiteSpace(municipio))
                {
                    return BadRequest("UF e município são obrigatórios.");
                }

                var vereadores = await _context.candidato
                    .Include(c => c.partidopolitico) // Inclui o partido político relacionado
                    .Where(c => c.cargodisputado.nome == "Vereador" && c.uf == uf && c.municipio == municipio)
                    .Select(c => new
                    {
                        c.nomeCompleto,
                        c.nomeUrna,
                        Partido = c.partidopolitico.nome,
                        SiglaPartido = c.partidopolitico.sigla
                    })
                    .ToListAsync();

                if (!vereadores.Any())
                {
                    return NotFound("Nenhum candidato a vereador encontrado para o UF e município especificados.");
                }

                return Ok(vereadores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar candidatos a vereador: {ex.Message}");
            }
        }


    }
}

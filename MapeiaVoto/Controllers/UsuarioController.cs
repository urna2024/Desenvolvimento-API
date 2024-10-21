using MapeiaVoto.Application.Dto;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Domain.Interfaces;
using MapeiaVoto.Infrastructure.Data.Context;
using MapeiaVoto.Service.Validators; // Importando o Validator do local correto
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
    public class UsuarioController : ControllerBase
    {
        private readonly IBaseService<Usuario> _baseService;
        private readonly SqlServerContext _context;
        private readonly UsuarioValidator _usuarioValidator; // Usando o Validator da camada de service

        public UsuarioController(IBaseService<Usuario> baseService, SqlServerContext context)
        {
            _baseService = baseService;
            _context = context;
            _usuarioValidator = new UsuarioValidator(_context); // Passando o contexto ao instanciar o Validator
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
        public async Task<ActionResult<Usuario>> Create(UsuarioDto request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var novoUsuario = new Usuario
                    {
                        nomeUsuario = request.nomeUsuario,
                        email = request.email,
                        senha = request.senha,
                        idStatus = request.idStatus,
                        idPerfilUsuario = request.idPerfilUsuario,
                        precisaTrocarSenha = true
                    };

                    // Validar o novoUsuario antes de salvar
                    var validationResult = _usuarioValidator.Validate(novoUsuario);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(validationResult.Errors);
                    }

                    _context.usuario.Add(novoUsuario);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var createdUsuario = await _context.usuario
                        .Include(u => u.status)
                        .Include(u => u.perfilusuario)
                        .FirstOrDefaultAsync(e => e.id == novoUsuario.id);

                    return CreatedAtAction(nameof(Create), new { id = createdUsuario.id }, createdUsuario);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    var innerExceptionMessage = ex.InnerException?.Message ?? "Nenhuma exceção interna";
                    var fullExceptionMessage = $"Erro ao criar Usuario: {ex.Message} | Exceção interna: {innerExceptionMessage}";
                    Console.WriteLine(fullExceptionMessage);
                    return StatusCode(StatusCodes.Status500InternalServerError, fullExceptionMessage);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UsuarioDto request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usuario = await _context.usuario
                        .FirstOrDefaultAsync(u => u.id == id);

                    if (usuario == null)
                    {
                        return NotFound("Usuário não encontrado.");
                    }

                    usuario.nomeUsuario = request.nomeUsuario;
                    usuario.email = request.email;
                    usuario.senha = request.senha;
                    usuario.idStatus = request.idStatus;
                    usuario.idPerfilUsuario = request.idPerfilUsuario;

                    // Validar o usuário antes de salvar
                    var validationResult = _usuarioValidator.Validate(usuario);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(validationResult.Errors);
                    }

                    _context.usuario.Update(usuario);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(usuario);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Usuário.");
                }
            }
        }

        [HttpGet("dadosBasicos")]
        public async Task<ActionResult<List<object>>> GetBasicUsuarioData()
        {
            try
            {
                var usuarios = await _context.usuario
                    .Include(u => u.status)
                    .Include(u => u.perfilusuario)
                    .Select(u => new
                    {
                        u.id,
                        u.nomeUsuario, // Incluído nomeUsuario
                        u.email, // Incluído email
                        u.idStatus,
                        statusNome = u.status.nome,
                        perfilusuario = u.perfilusuario.nome
                    })
                    .ToListAsync();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar os dados básicos dos usuários.");
            }
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usuarioExistente = await _context.usuario.FirstOrDefaultAsync(u => u.id == id);

                    if (usuarioExistente == null)
                    {
                        return NotFound("Usuário não encontrado.");
                    }

                    _context.usuario.Remove(usuarioExistente);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok("Usuário excluído com sucesso.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir Usuário.");
                }
            }
        }

        [HttpPatch("{id}/mudarStatus")]
        public async Task<IActionResult> MudarStatus(int id, [FromBody] int novoStatusId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usuario = await _context.usuario.FirstOrDefaultAsync(u => u.id == id);
                    if (usuario == null)
                    {
                        return NotFound(new { Message = "Usuário não encontrado." });
                    }

                    var status = await _context.status.FirstOrDefaultAsync(s => s.id == novoStatusId);
                    if (status == null)
                    {
                        return NotFound(new { Message = "Status não encontrado." });
                    }

                    usuario.idStatus = novoStatusId;

                    _context.usuario.Update(usuario);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(usuario);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Erro ao mudar o status do usuário.", Details = ex.Message });
                }
            }
        }

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

        [HttpGet("tipoPerfilUsuario")]
        public IActionResult ObterTiposPerfilUsuario()
        {
            try
            {
                var tiposPerfilUsuario = _context.perfilusuario.ToList();
                return Ok(tiposPerfilUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/dadosCompletos")]
        public async Task<ActionResult<Usuario>> GetCompleteUsuarioData(int id)
        {
            try
            {
                var usuario = await _context.usuario
                    .Include(p => p.status)
                    .Include(p => p.perfilusuario)
                    .FirstOrDefaultAsync(p => p.id == id);

                if (usuario == null)
                {
                    return NotFound("Usuario não encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar os dados completos do usuario.");
            }
        }

        [HttpGet]
        public IActionResult SelecionarTodos()
        {
            return Execute(() => _baseService.Get<Usuario>());
        }
    }
}

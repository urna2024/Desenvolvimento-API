using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Application.Dto;
using MapeiaVoto.Infrastructure.Data.Context;
using partymanager.Application.Dto;

[Route("api/[controller]")]
[ApiController]
public class SegurancaController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly SqlServerContext _context; // Aqui estou assumindo que você está usando `SqlServerContext`

    public SegurancaController(IConfiguration config, SqlServerContext context)
    {
        _config = config;
        _context = context;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDetalhes)
    {
        var usuario = await ValidarUsuarioAsync(loginDetalhes.email, loginDetalhes.senha);
        if (usuario != null)
        {
            // Verifica se o usuário precisa trocar a senha
            if (usuario.precisaTrocarSenha)
            {
                // Inclua o ID do usuário na resposta
                return Ok(new { mensagem = "Troca de senha obrigatória.", precisaTrocarSenha = true, id = usuario.id });
            }

            // Gerar o token JWT se o usuário for válido e não precisar trocar a senha
            var tokenString = GerarTokenJWT(usuario);

            // Inclua o ID do usuário na resposta junto com o token
            return Ok(new { token = tokenString, id = usuario.id });
        }
        else
        {
            return Unauthorized("Email ou senha incorretos.");
        }
    }

    [HttpPost("TrocarSenha")]
    public async Task<IActionResult> TrocarSenha([FromBody] UsuarioTrocarSenhaDto senhaDto)
    {
        var usuario = await _context.usuario.FirstOrDefaultAsync(u => u.id == senhaDto.id);

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        // Verifica se a senha atual está correta
        if (usuario.senha != senhaDto.senha)
        {
            return BadRequest("A senha atual está incorreta.");
        }

        // Atualiza a senha e define que não precisa mais trocar
        usuario.senha = senhaDto.novaSenha;
        usuario.precisaTrocarSenha = false;

        // Salva as alterações no banco de dados
        _context.usuario.Update(usuario);
        await _context.SaveChangesAsync();

        return Ok("Senha alterada com sucesso.");
    }

    private string GerarTokenJWT(Usuario usuario)
    {
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.email),
            new Claim(JwtRegisteredClaimNames.Email, usuario.email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("nome", usuario.nomeUsuario) // O nome será adicionado ao token JWT
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(120), // Token válido por 120 minutos
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private async Task<Usuario> ValidarUsuarioAsync(string email, string senha)
    {
        // Busca o usuário no banco de dados através do email
        var usuario = await _context.usuario.FirstOrDefaultAsync(u => u.email == email);

        if (usuario != null && usuario.senha == senha) // Verifica se a senha está correta
        {
            return usuario; // Retorna o usuário se o email e a senha forem válidos
        }

        return null; // Retorna null se a validação falhar
    }
}

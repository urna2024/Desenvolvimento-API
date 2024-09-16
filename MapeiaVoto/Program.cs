using AutoMapper;
using MapeiaVoto.Application.Models;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Domain.Interfaces;
using MapeiaVoto.Infrastructure.Data.Context;
using MapeiaVoto.Infrastructure.Data.Repository;
using MapeiaVoto.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner

var configuration = builder.Configuration;

// Configura��o do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configura��o da Autentica��o JWT
var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddControllers();

// Configura��o do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MapeiaVoto API", Version = "v1" });
});

// Configura��o do JSON
builder.Services.AddMvc().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Configura��o do AutoMapper
builder.Services.AddSingleton(new MapperConfiguration(config =>
{
    config.CreateMap<Status, StatusModel>();
    config.CreateMap<StatusModel, Status>();

    config.CreateMap<Genero, GeneroModel>();
    config.CreateMap<GeneroModel, Genero>();

    config.CreateMap<PerfilUsuario, PerfilUsuarioModel>();
    config.CreateMap<PerfilUsuarioModel, PerfilUsuario>();

    config.CreateMap<PartidoPolitico, PartidoPoliticoModel>();
    config.CreateMap<PartidoPoliticoModel, PartidoPolitico>();

    config.CreateMap<Candidato, CandidatoModel>();
    config.CreateMap<CandidatoModel, Candidato>();

    config.CreateMap<CargoDisputado, CargoDisputadoModel>();
    config.CreateMap<CargoDisputadoModel, CargoDisputado>();


    config.CreateMap<Usuario, UsuarioModel>();
    config.CreateMap<UsuarioModel, Usuario>();



}).CreateMapper());

// Configura��o dos servi�os e reposit�rios
builder.Services.AddDbContext<SqlServerContext>();

builder.Services.AddScoped<IBaseService<Status>, BaseService<Status>>();
builder.Services.AddScoped<IBaseRepository<Status>, BaseRepository<Status>>();

builder.Services.AddScoped<IBaseService<Genero>, BaseService<Genero>>();
builder.Services.AddScoped<IBaseRepository<Genero>, BaseRepository<Genero>>();

builder.Services.AddScoped<IBaseService<PerfilUsuario>, BaseService<PerfilUsuario>>();
builder.Services.AddScoped<IBaseRepository<PerfilUsuario>, BaseRepository<PerfilUsuario>>();

builder.Services.AddScoped<IBaseService<PartidoPolitico>, BaseService<PartidoPolitico>>();
builder.Services.AddScoped<IBaseRepository<PartidoPolitico>, BaseRepository<PartidoPolitico>>();

builder.Services.AddScoped<IBaseService<Candidato>, BaseService<Candidato>>();
builder.Services.AddScoped<IBaseRepository<Candidato>, BaseRepository<Candidato>>();

builder.Services.AddScoped<IBaseService<CargoDisputado>, BaseService<CargoDisputado>>();
builder.Services.AddScoped<IBaseRepository<CargoDisputado>, BaseRepository<CargoDisputado>>();

builder.Services.AddScoped<IBaseService<Usuario>, BaseService<Usuario>>();
builder.Services.AddScoped<IBaseRepository<Usuario>, BaseRepository<Usuario>>();


var app = builder.Build();

// Configura��o do pipeline de requisi��o

// Exibindo Swagger tanto em desenvolvimento quanto em produ��o
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MapeiaVoto API v1"));

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve arquivos est�ticos da pasta wwwroot por padr�o

app.UseRouting();

// Ativando CORS
app.UseCors("CorsPolicy");

// Ativando Autentica��o e Autoriza��o
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



//var app = builder.Build();

//// Configura��o do pipeline de requisi��o

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MapeiaVoto API v1"));
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles(); // Serve arquivos est�ticos da pasta wwwroot por padr�o

//app.UseRouting();

//// Ativando CORS
//app.UseCors("CorsPolicy");

//// Ativando Autentica��o e Autoriza��o
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();


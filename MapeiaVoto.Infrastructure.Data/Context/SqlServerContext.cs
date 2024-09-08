using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace MapeiaVoto.Infrastructure.Data.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Entrevistado> Entrevistados { get; set; }
        public DbSet<Status> status { get; set; }
        public DbSet<Genero> genero { get; set; }
        public DbSet<PerfilUsuario> perfilusuario { get; set; }
        public DbSet<PartidoPolitico> partidopolitico { get; set; }
        public DbSet<NivelEscolaridade> nivelescolaridade { get; set; }
        public DbSet<Candidato> candidato { get; set; }
        public DbSet<CargoDisputado> cargodisputado { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var stringConexao = @"Server=GUSTACNOTE;DataBase=MapeiaVotoV03;integrated security=true;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(stringConexao);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EntrevistadoMap());
            modelBuilder.ApplyConfiguration(new StatusMap());
            modelBuilder.ApplyConfiguration(new GeneroMap());
            modelBuilder.ApplyConfiguration(new PerfilUsuarioMap());
            modelBuilder.ApplyConfiguration(new PartidoPoliticoMap());
            modelBuilder.ApplyConfiguration(new NivelEscolaridadeMap());
            modelBuilder.ApplyConfiguration(new CandidatoMap());
            modelBuilder.ApplyConfiguration(new CargoDisputadoMap());
        }
    }

    // Fábrica para permitir a criação de DbContext em tempo de design
    public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
    {
        public SqlServerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerContext>();
            var stringConexao = @"Server=GUSTACNOTE;DataBase=MapeiaVotoV03;integrated security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(stringConexao);

            return new SqlServerContext(optionsBuilder.Options);
        }
    }
}

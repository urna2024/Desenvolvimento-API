using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MapeiaVoto.Domain.Entidades;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario"); // Nome da tabela no SqlServer

            builder.HasKey(prop => prop.id); // Chave primária no SqlServer

            builder.Property(prop => prop.nomeUsuario)
                .IsRequired()
                .HasColumnName("nomeUsuario")
                .HasColumnType("varchar(255)");

            // Propriedade email
            builder.Property(u => u.email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("varchar(255)");

            builder.Property(prop => prop.senha)
                .IsRequired()
                .HasColumnName("senha")
                .HasColumnType("varchar(255)");

            // Mapeamento da nova coluna PrecisaTrocarSenha
            builder.Property(prop => prop.precisaTrocarSenha)
                .IsRequired() // Campo obrigatório
                .HasDefaultValue(true) // Valor padrão true para novos usuários
                .HasColumnName("precisaTrocarSenha") // Nome da coluna no banco de dados
                .HasColumnType("bit"); // Tipo de dado booleano no SQL Server

            // Relação com a tabela Status
            builder.HasOne(r => r.status)
                .WithMany(s => s.usuario)
                .HasForeignKey(r => r.idStatus)
                .OnDelete(DeleteBehavior.Restrict);

            // Relação com a tabela PerfilUsuario
            builder.HasOne(r => r.perfilusuario)
                .WithMany(p => p.usuario)
                .HasForeignKey(r => r.idPerfilUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Definir a relação com Representante
            builder.HasMany(s => s.pesquisaeleitoralmunicipal)
                .WithOne(r => r.usuario)
                .HasForeignKey(r => r.idUsuario)
                .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção
        }
    }
}

using MapeiaVoto.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class CandidatoMap : IEntityTypeConfiguration<Candidato>
    {
        public void Configure(EntityTypeBuilder<Candidato> builder)
        {
            builder.ToTable("Candidato");

            
            builder.HasKey(e => e.id);

           
            builder.Property(e => e.nomeCompleto)
                .IsRequired() 
                .HasMaxLength(150);

            builder.Property(e => e.nomeUrna)
               .IsRequired()
               .HasMaxLength(150);


            builder.Property(e => e.dataNascimento)
                .IsRequired(); 

            
            builder.Property(e => e.uf)
                .IsRequired() 
                .HasMaxLength(2); 

            
            builder.Property(e => e.municipio)
                .IsRequired() 
                .HasMaxLength(100); 

           
            builder.Property(e => e.foto)
                .HasMaxLength(255);

            // Definir a relação com Status
            builder.HasOne(r => r.status)
                .WithMany(s => s.candidato)
                .HasForeignKey(r => r.idStatus)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.cargodisputado)
                .WithMany(s => s.candidato)
                .HasForeignKey(r => r.idCargoDisputado)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.partidopolitico)
                .WithMany(s => s.candidato)
                .HasForeignKey(r => r.idPartidoPolitico)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

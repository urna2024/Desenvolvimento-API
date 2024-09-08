using MapeiaVoto.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class CargoDisputadoMap : IEntityTypeConfiguration<CargoDisputado>
    {
        public void Configure(EntityTypeBuilder<CargoDisputado> builder)
        {
            builder.ToTable("CargoDisputado");


            builder.HasKey(e => e.id);


            builder.Property(e => e.nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasMany(s => s.candidato)
               .WithOne(r => r.cargodisputado)
               .HasForeignKey(r => r.idCargoDisputado)
               .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção
        }
    }
}

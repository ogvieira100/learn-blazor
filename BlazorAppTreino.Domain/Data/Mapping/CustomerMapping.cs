using BlazorAppTreino.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Data.Mapping
{
   public class CustomerMapping:BaseMapping<Customers>
   {
        public override void Configure(EntityTypeBuilder<Customers> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.CPF)
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(p => p.Telphone)
               .HasColumnName("Telefone")
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(p => p.Name)
                .HasColumnName("Nome")
                .HasMaxLength(100)
                .IsRequired();


            builder.Property(p => p.SurName)
                .HasColumnName("SobreNome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnName("Email")
                .HasMaxLength(100)
                .IsRequired();

            builder.ToTable("Cliente");
        }
    }
}

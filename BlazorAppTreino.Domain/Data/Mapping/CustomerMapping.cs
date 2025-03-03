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
                .IsRequired();

            builder.ToTable("cliente");
        }
    }
}

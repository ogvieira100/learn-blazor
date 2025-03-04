using BlazorAppTreino.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Data.Mapping
{
    public class AddressMapping : BaseMapping<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Street)
                .HasColumnName("Logradouro")
                .HasMaxLength(200)
                .IsRequired()
                ;

            builder.Property(x => x.Number)
                .HasColumnName("Numero")
                .HasMaxLength(50)
                .IsRequired()
                ;

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired(false);

            builder.ToTable("Endereco");
        }
    }
}

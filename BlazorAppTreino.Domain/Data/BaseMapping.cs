using BlazorAppTreino.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Data
{
    public abstract class BaseMapping<T> : IEntityTypeConfiguration<T> where T : EntityDataBase
    {

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Active)
                    .HasColumnName("Active")
                    .IsRequired(true)
                    ;

            builder.Property(c => c.DateRegister)
                   .HasColumnName("DateRegister")
                   .IsRequired(true)
                   ;

            builder.Property(c => c.DateUpdate)
                    .HasColumnName("DateUpdate")
                    .IsRequired(false)
                    ;

            builder.Property(c => c.UserInsertedId)
                  .HasColumnName("UserInsertedId")
                  .IsRequired(true)
                  ;

            builder.Property(c => c.UserUpdatedId)
                   .HasColumnName("UserUpdatedId")
                   .IsRequired(false)
                   ;

            builder.Property(c => c.DeleteDate)
                   .HasColumnName("DeleteDate")
                   .IsRequired(false)
                   ;
            builder.Property(c => c.UserDeletedId)
                    .HasColumnName("UserDeletedId")
                    .IsRequired(false)
                    ;

        }
    }
}

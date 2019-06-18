using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class FormTaskConfiguration : IEntityTypeConfiguration<FormTaskEntity>
    {
        public void Configure(EntityTypeBuilder<FormTaskEntity> builder)
        {
            builder.ToTable("FormTasks")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.FileName)
               .IsRequired(false);

            builder.Property(x => x.LastUpdated)
                .IsRequired(false);

            builder
                .HasOne(x => x.Form)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.FormId)
                .IsRequired();
        }
    }
}

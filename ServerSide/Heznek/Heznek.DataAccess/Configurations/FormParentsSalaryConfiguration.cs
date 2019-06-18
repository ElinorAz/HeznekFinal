using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class FormParentsSalaryConfiguration : IEntityTypeConfiguration<FormParentsSalaryEntity>
    {
        public void Configure(EntityTypeBuilder<FormParentsSalaryEntity> builder)
        {
            builder.ToTable("FormsParentsSalary")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.MotherName)
                .IsRequired(false);

            builder.Property(x => x.FatherName)
                .IsRequired(false);

            builder.Property(x => x.SalarySlips)
                .IsRequired(false);

            builder.Property(x => x.Disability)
                .IsRequired(false);

            builder.Property(x => x.Disability2)
                .IsRequired(false);

            builder.Property(x => x.LastUpdated)
                .IsRequired(false);

            builder.Property(x => x.FatherDisability)
                .IsRequired(true);

            builder.Property(x => x.MotherDisability)
               .IsRequired(true);

            builder
                .HasOne(x => x.Form)
                .WithOne(x => x.ParentsSalary)
                .HasPrincipalKey<FormEntity>(x => x.Id)
                .HasForeignKey<FormParentsSalaryEntity>(x => x.FormId)
                .IsRequired();
        }
    }
}

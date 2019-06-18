using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class AcademicStudiesConfiguration : IEntityTypeConfiguration<AcademicStudiesEntity>
    {
        public void Configure(EntityTypeBuilder<AcademicStudiesEntity> builder)
        {
            builder.ToTable("AcademicStudies")
                .HasKey(x => x.Id);

            builder.Property(x => x.AcademicDegree)
                .IsRequired(false);

            builder.Property(x => x.FieldOfStudy)
                .IsRequired(false);

            builder.Property(x => x.AcademicInstitution)
                .IsRequired(false);

            builder.Property(x => x.Residence)
                .IsRequired(false);

            builder.Property(x => x.GraduationYear)
                .IsRequired(false);

            builder.Property(x => x.AprovalFileName)
                .IsRequired(false);

            builder.Property(x => x.GradesFileName)
                .IsRequired(false);

            builder.Property(x => x.AprovalDownloadName)
                .IsRequired(false);

            builder.Property(x => x.GradesDownloadName)
                .IsRequired(false);

            builder
                .HasOne(x => x.Profile)
                .WithOne(x => x.AcademicStudies)
                .HasPrincipalKey<ProfileEntity>(x => x.Id)
                .HasForeignKey<AcademicStudiesEntity>(x=>x.Id);
        }                           
    }
}

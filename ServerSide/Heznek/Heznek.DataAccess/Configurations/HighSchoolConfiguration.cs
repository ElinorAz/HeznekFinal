using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class HighSchoolConfiguration : IEntityTypeConfiguration<HighSchoolEntity>
    {
        public void Configure(EntityTypeBuilder<HighSchoolEntity> builder)
        {
            builder.ToTable("HighSchools")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired(false);

            builder.Property(x => x.Year)
                .IsRequired(false);

            builder
                .HasOne(x => x.Profile)
                .WithOne(x => x.HighSchool)
                .HasPrincipalKey<ProfileEntity>(x => x.Id)
                .HasForeignKey<HighSchoolEntity>(x => x.Id);
        }
    }

}

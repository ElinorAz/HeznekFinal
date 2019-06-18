using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class GeneralConfiguration : IEntityTypeConfiguration<GeneralEntity>
    {
        public void Configure(EntityTypeBuilder<GeneralEntity> builder)
        {
            builder.ToTable("Generals")
                .HasKey(x => x.Id);

            builder.Property(x => x.PsychometricGrade)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.WorthyOfAdvancment)
                .IsRequired();

            builder.Property(x => x.Points)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.Disabilities)
                .IsRequired();


            builder
                .HasOne(x => x.Profile)
                .WithOne(x => x.General)
                .HasPrincipalKey<ProfileEntity>(x => x.Id)
                .HasForeignKey<GeneralEntity>(x => x.Id);
        }
    }

}

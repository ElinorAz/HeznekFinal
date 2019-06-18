using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{

    public class MilitaryServiceConfiguration : IEntityTypeConfiguration<MilitaryServiceEntity>
    {
        public void Configure(EntityTypeBuilder<MilitaryServiceEntity> builder)
        {
            builder.ToTable("MilitaryServices")
                .HasKey(x => x.Id);

            builder.Property(x => x.Role)
                .IsRequired(false);

            builder.Property(x => x.TypeOfSevice)
                .IsRequired(false);

            builder.Property(x => x.EaseOfService)
                .IsRequired();

            builder.Property(x => x.Ease)
                .IsRequired(false);

            builder.Property(x => x.Details)
                .IsRequired(false);

            builder
                .HasOne(x => x.Profile)
                .WithOne(x => x.MilitaryService)
                .HasPrincipalKey<ProfileEntity>(x => x.Id)
                .HasForeignKey<MilitaryServiceEntity>(x => x.Id);
        }
    }
}

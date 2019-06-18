using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class VolunteerDetailsConfiguration : IEntityTypeConfiguration<VolunteerDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<VolunteerDetailsEntity> builder)
        {
            builder.ToTable("VolunteerDetails")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Contribution)
                .IsRequired(false);

            builder
                .Property(x => x.Hours)
                .IsRequired();

            builder
               .HasOne(x => x.Profile)
               .WithOne(x => x.VolunteerDetails)
               .HasPrincipalKey<ProfileEntity>(x => x.Id)
               .HasForeignKey<VolunteerDetailsEntity>(x => x.Id);
        }
    }
}

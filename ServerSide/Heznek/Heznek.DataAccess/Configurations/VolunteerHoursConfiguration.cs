using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class VolunteerHoursConfiguration : IEntityTypeConfiguration<VolunteerHoursEntity>
    {
        public void Configure(EntityTypeBuilder<VolunteerHoursEntity> builder)
        {
            builder.ToTable("VolunteerHours")
                .HasKey(x => x.Id);

            builder.Property(x => x.ActivityType)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Start)
                .IsRequired();

            builder.Property(x => x.End)
                .IsRequired();

            builder.Property(x => x.Semester)
                .IsRequired();

            builder
                .HasOne(x => x.Profile)
                .WithMany(x => x.VolunteerHours)
                .HasForeignKey(x => x.ProfileId)
                .IsRequired();
        }
    }
}

using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class StudentScholarshipConfiguration : IEntityTypeConfiguration<StudentScholarshipEntity>
    {
        public void Configure(EntityTypeBuilder<StudentScholarshipEntity> builder)
        {
            builder.ToTable("StudentsScholarships")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.GivenInPast)
                .IsRequired();

            builder
                .HasOne(x => x.Scholarship)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.ScholarshipId)
                .IsRequired();

            builder
                .HasOne(x => x.Profile)
                .WithMany(x => x.Scholarships)
                .HasForeignKey(x => x.ProfileId)
                .IsRequired();

        }
    }
}

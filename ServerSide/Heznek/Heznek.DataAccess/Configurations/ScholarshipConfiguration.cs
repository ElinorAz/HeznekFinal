using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class ScholarshipConfiguration : IEntityTypeConfiguration<ScholarshipEntity>
    {
        public void Configure(EntityTypeBuilder<ScholarshipEntity> builder)
        {
            builder.ToTable("Scholarship")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Budget)
                .IsRequired();

            builder.Property(x => x.Admission)
                .IsRequired();

        }
    }
}

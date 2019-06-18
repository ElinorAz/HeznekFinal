using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class ScholarDetailsConfiguration : IEntityTypeConfiguration<ScholarDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<ScholarDetailsEntity> builder)
        {
            builder.ToTable("ScholarDetails")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Refund)
                .IsRequired();

            builder
                .Property(x => x.Budgeting)
                .IsRequired();

            builder
                .Property(x => x.Amount)
                .IsRequired();

            builder
               .HasOne(x => x.Profile)
               .WithOne(x => x.ScholarDetails)
               .HasPrincipalKey<ProfileEntity>(x => x.Id)
               .HasForeignKey<ScholarDetailsEntity>(x => x.Id);
        }
    }
}

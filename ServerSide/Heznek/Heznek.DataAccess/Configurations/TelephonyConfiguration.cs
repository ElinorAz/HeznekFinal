using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class TelephonyConfiguration : IEntityTypeConfiguration<TelephonyEntity>
    {
        public void Configure(EntityTypeBuilder<TelephonyEntity> builder)
        {
            builder.ToTable("Telephonies")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Remarks)
                .IsRequired(false);

            builder
                .Property(x => x.Thoughts)
                .IsRequired(false);

            builder
                .Property(x => x.FundingAvailability)
                .IsRequired();

            builder
                .Property(x => x.DateBackFirst)
                .IsRequired(false);

            builder
                .Property(x => x.DateBackSecond)
                .IsRequired(false);

            builder
                .Property(x => x.DateBackThird)
                .IsRequired(false);

            builder
                .HasOne(x => x.Profile)
                .WithOne(x => x.Telephony)
                .HasPrincipalKey<ProfileEntity>(x => x.Id)
                .HasForeignKey<TelephonyEntity>(x => x.Id);

        }
    }
}

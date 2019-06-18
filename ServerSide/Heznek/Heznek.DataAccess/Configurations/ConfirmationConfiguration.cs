using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class ConfirmationConfiguration : IEntityTypeConfiguration<ConfirmationEntity>
    {
        public void Configure(EntityTypeBuilder<ConfirmationEntity> builder)
        {
            builder.ToTable("Confirmations")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Confirmed)
                .IsRequired();

            builder
                .Property(x => x.Code)
                .IsRequired();

            builder
                .Property(x => x.ConfirmDate)
                .IsRequired(false);

            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Confirmation).HasPrincipalKey<UserEntity>(x=>x.Id).HasForeignKey<ConfirmationEntity>(x=>x.Id);

        }
    }
}

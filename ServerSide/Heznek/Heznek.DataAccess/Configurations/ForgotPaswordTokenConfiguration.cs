using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Heznek.DataAccess.Entities;

namespace Heznek.DataAccess.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<ForgotPaswordTokenEntity>
    {
        public void Configure(EntityTypeBuilder<ForgotPaswordTokenEntity> builder)
        {
            builder.ToTable("ForgotPaswordTokens")
                .HasKey(x=>x.Id);

            builder
                .Property(x => x.Code)
                .IsRequired();

            builder
                .Property(x => x.ExpireTime)
                .IsRequired();

            builder
                .Property(x => x.Used)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.ForgotPaswordTokens)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}
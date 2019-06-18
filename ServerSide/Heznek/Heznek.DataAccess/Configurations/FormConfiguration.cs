using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class FormConfiguration : IEntityTypeConfiguration<FormEntity>
    {
        public void Configure(EntityTypeBuilder<FormEntity> builder)
        {
            builder.ToTable("Forms")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Status)
                .HasDefaultValue(FormStatusEnum.InProgress)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Form)
                .HasPrincipalKey<UserEntity>(x => x.Id)
                .HasForeignKey<FormEntity>(x => x.UserId)
                .IsRequired();
        }
    }
}

using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<ProfileEntity>
    {
        public void Configure(EntityTypeBuilder<ProfileEntity> builder)
        {
            builder.ToTable("Profiles")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.City)
                .IsRequired(false);

            builder
                .Property(x => x.Gender)
                .IsRequired(false);

            builder
                .Property(x => x.BirthDate)
                .IsRequired(false);

            builder
                .Property(x => x.Address)
                .IsRequired(false);

            builder
                .Property(x => x.Phone)
                .IsRequired(false);

            builder
                .Property(x => x.Siblings)
                .HasDefaultValue(0)
                .IsRequired();

            builder
                .Property(x => x.AcademicParents)
                .HasDefaultValue(0)
                .IsRequired();

            builder
                .Property(x => x.Status)
                .HasDefaultValue(UserStatusEnum.ActiveCandidate)
                .IsRequired();
                
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Profile)
                .HasPrincipalKey<UserEntity>(x => x.Id)
                .HasForeignKey<ProfileEntity>(x => x.UserId)
                .IsRequired();
        }
    }
}

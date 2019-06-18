using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class BankInfoConfiguration : IEntityTypeConfiguration<BankInfoEntity>
    {
        public void Configure(EntityTypeBuilder<BankInfoEntity> builder)
        {
            builder.ToTable("BanksInfo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.BankName)
                .IsRequired(false);

            builder
                .Property(x => x.BranchNumber)
                .IsRequired(false);

            builder
                .Property(x => x.AccountNumber)
                .IsRequired(false);

            builder
                .HasOne(x => x.Profile)
                .WithOne(x => x.BankInfo)
                .HasPrincipalKey<ProfileEntity>(x => x.Id)
                .HasForeignKey<BankInfoEntity>(x => x.Id);
        }
    }
}

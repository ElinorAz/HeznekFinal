using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class CandidateAdditionalDataConfiguration : IEntityTypeConfiguration<CandidateAdditionalDataEntity>
    {
        public void Configure(EntityTypeBuilder<CandidateAdditionalDataEntity> builder)
        {
            builder.ToTable("CandidateAdditionalData")
                .HasKey(x => x.Id);

            builder.Property(x => x.ParticipationDescription)
                .IsRequired(false);

            builder.Property(x => x.Reason)
                .IsRequired(false);

            builder.Property(x => x.Difficulties)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .IsRequired(false);

            builder.Property(x => x.LifeStory)
                .IsRequired(false);

            builder.Property(x => x.SituationDetails)
                .IsRequired(false);


            builder.Property(x => x.HealthProblems)
                .IsRequired(false);

            builder.Property(x => x.FinancialProblems)
                .IsRequired(false);

            builder.Property(x => x.FamilyDifficulties)
                .IsRequired(false);

            builder.Property(x => x.HasHealthProblems)
               .IsRequired();

            builder.Property(x => x.HasFinancialProblems)
               .IsRequired();

            builder.Property(x => x.HasFamilyDifficulties)
               .IsRequired();

            builder
                .HasOne(x => x.Profile)
                .WithOne(x => x.CandidateAdditionalData)
                .HasPrincipalKey<ProfileEntity>(x => x.Id)
                .HasForeignKey<CandidateAdditionalDataEntity>(x => x.Id);
        }
    }
}

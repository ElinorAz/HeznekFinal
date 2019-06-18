using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class GeneralParticipationConfiguration : IEntityTypeConfiguration<GeneralParticipationEntity>
    {
        public void Configure(EntityTypeBuilder<GeneralParticipationEntity> builder)
        {
            builder.ToTable("GeneralsParticipations")
                .HasKey(x=>x.Id);

            builder
                .Property(x => x.Description)
                .IsRequired(false);

            builder
                .HasOne(x => x.General)
                .WithMany(x => x.Participations)
                .HasForeignKey(x => x.GeneralId)
                .IsRequired();

            builder
                .HasOne(x => x.Program)
                .WithMany(x => x.Generals)
                .HasForeignKey(x => x.ProgramId)
                .IsRequired();
        }
    }
}

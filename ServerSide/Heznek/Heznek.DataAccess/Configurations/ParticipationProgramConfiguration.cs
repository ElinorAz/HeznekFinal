using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class ParticipationProgramConfiguration : IEntityTypeConfiguration<ParticipationProgramEntity>
    {
        public void Configure(EntityTypeBuilder<ParticipationProgramEntity> builder)
        {
            builder.ToTable("ParticipationPrograms")
                .HasKey(x => x.Id);

            builder.Property(x => x.ProgramName)
                .IsRequired();
        }
    }
}

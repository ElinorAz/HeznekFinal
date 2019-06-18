using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class EventParticipantTypeConfiguration : IEntityTypeConfiguration<EventParticipantTypeEntity>
    {
        public void Configure(EntityTypeBuilder<EventParticipantTypeEntity> builder)
        {
            builder.ToTable("EventParticipantTypes")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.UserType)
                .IsRequired();

            builder
                .HasOne(x => x.Event)
                .WithMany(x => x.ParticipantTypes)
                .HasForeignKey(x => x.EventId)
                .IsRequired();
        }
    }
}

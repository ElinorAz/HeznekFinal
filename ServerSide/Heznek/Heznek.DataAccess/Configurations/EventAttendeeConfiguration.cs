using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class EventAttendeeConfiguration : IEntityTypeConfiguration<EventAttendeeEntity>
    {
        public void Configure(EntityTypeBuilder<EventAttendeeEntity> builder)
        {
            builder.ToTable("EventAttendees")
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Event)
                .WithMany(x => x.Attendees)
                .HasForeignKey(x => x.EventId)
                .IsRequired();

            builder
                .HasOne(x => x.Profile)
                .WithMany(x => x.AttendingEvents)
                .HasForeignKey(x => x.ProfileId)
                .IsRequired();
        }
    }
}

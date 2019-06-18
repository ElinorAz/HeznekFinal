using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.ToTable("Events")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Subject)
                .IsRequired();

            builder
                .Property(x => x.Date)
                .IsRequired();

            builder
                .Property(x => x.Time)
                .IsRequired();

            builder
                .Property(x => x.FinishTime)
                .IsRequired();

            builder
                .Property(x => x.Location)
                .IsRequired();

            builder
                .Property(x => x.Expected)
                .IsRequired();
        }
    }
}

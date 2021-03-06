﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Heznek.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Heznek.DataAccess
{
    public class HeznekDbContext : DbContext
    {
        public HeznekDbContext(DbContextOptions<HeznekDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterFoundEntityConfigs(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void RegisterFoundEntityConfigs(ModelBuilder mb)
        {
            AutoRegisterEntityConfigs(mb);

            foreach (var relationship in mb.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private void AutoRegisterEntityConfigs(ModelBuilder mb)
        {
            var entityConfigTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.GetInterfaces()
                                        .FirstOrDefault(y => y.IsGenericType && y.GetGenericTypeDefinition().UnderlyingSystemType == typeof(IEntityTypeConfiguration<>)) != null);

            MethodInfo genericAddConfig = typeof(ModelBuilder).GetMethod(nameof(ModelBuilder.ApplyConfiguration));

            foreach (Type configurationType in entityConfigTypes)
            {
                Type entityType = configurationType.GetTypeInfo().ImplementedInterfaces.First().GetGenericArguments().First();
                object configurationInstance = Activator.CreateInstance(configurationType);
                MethodInfo concreteAddConfig = genericAddConfig.MakeGenericMethod(entityType);
                concreteAddConfig.Invoke(mb, new object[] { configurationInstance });
            }
        }

        public void EnsureSeeded()
        {
             AddParticipationPrograms();
        }

        private void AddParticipationPrograms()
        {
            var entities = this.Set<ParticipationProgramEntity>();
            if(!entities.Any())
            {
                entities.AddRange(new ParticipationProgramEntity { ProgramName = "הזנק" },
                                    new ParticipationProgramEntity { ProgramName = "הזנק לתעשייה" },
                                    new ParticipationProgramEntity { ProgramName = "הזנק מרכנתיל" },
                                    new ParticipationProgramEntity { ProgramName = "הזנק לעתיד" });
                this.SaveChanges();

            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using GeekStore.Core.Dto_s;
using System;
using System.Linq;
using GeekStore.Core.Interfaces.BuildingBlocks;

namespace GeekStore.Core.Extentions
{
    public static class EntityFrameworkExtentions
    {
        public static ChangeTracker UpdateData(this ChangeTracker changeTracker, ISessionApp appSession)
        {
            foreach (var entry in changeTracker.Entries().Where(entry => entry.Entity is EntityBase))
            {
                var date = DateTime.UtcNow.RemoverMilisSegundos();

                entry.Property("IdUsuarioAlteracao").CurrentValue = appSession.GetUserId();
                entry.Property("DataAlteracao").CurrentValue = date;

                if (entry.State == EntityState.Added)
                {
                    entry.Property("IdUsuarioInclusao").CurrentValue = appSession.GetUserId();
                    entry.Property("DataInclusao").CurrentValue = date;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("IdUsuarioInclusao").IsModified = false;
                    entry.Property("DataInclusao").IsModified = false;

                    entry.Property("IdUsuarioAlteracao").CurrentValue = appSession.GetUserId();
                    entry.Property("DataAlteracao").CurrentValue = DateTime.UtcNow.RemoverMilisSegundos();
                }
            }

            return changeTracker;
        }

        public static ModelBuilder UpdateData(this ModelBuilder modelBuilder, Type type)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(type.Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            return modelBuilder;
        }
    }
}
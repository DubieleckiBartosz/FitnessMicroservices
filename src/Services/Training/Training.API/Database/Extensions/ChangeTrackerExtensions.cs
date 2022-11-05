using Fitness.Common.Projection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Training.API.Database.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SetAuditProperties(this ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            var entities =
                changeTracker
                    .Entries()
                    .Where(t => t.Entity is IRead && t.State == EntityState.Deleted)?.ToList();

            if (entities == null || !entities.Any())
            {
                return;
            }

            foreach (var entry in entities)
            {
                IRead entity = (IRead)entry.Entity;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}

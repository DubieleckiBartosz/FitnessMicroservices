using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Training.API.Database.Extensions;

public static class ChangeTrackerExtensions
{
    public static void SetAuditProperties(this ChangeTracker changeTracker)
    {
        changeTracker.DetectChanges();
        var entities =
            changeTracker
                .Entries()
                .Where(t => t.Entity is ITrainingRead && t.State == EntityState.Deleted)?.ToList();

        if (entities == null || !entities.Any())
        {
            return;
        }

        foreach (var entry in entities)
        {
            var entity = (ITrainingRead) entry.Entity;
            entity.IsDeleted = true;
            entry.State = EntityState.Modified;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Opinion.API.Domain;
using Opinion.API.Domain.Common;

namespace Opinion.API.Infrastructure.Database;

public class OpinionContext : DbContext
{
    public DbSet<Domain.Opinion> Opinions { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
     
    public OpinionContext(DbContextOptions<OpinionContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).Modified = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).Created = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
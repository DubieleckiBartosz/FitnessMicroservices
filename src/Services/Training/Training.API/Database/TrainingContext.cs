using Microsoft.EntityFrameworkCore;
using Training.API.Database.Extensions;

namespace Training.API.Database;

public class TrainingContext : DbContext
{ 
    public DbSet<TrainingDetails> Trainings { get; set; } 
    public DbSet<TrainingUser> TrainingUsers { get; set; }
    public DbSet<TrainingExercise> TrainingExercises { get; set; }

    public TrainingContext(DbContextOptions<TrainingContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly); 
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.SetAuditProperties();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        ChangeTracker.SetAuditProperties();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
}
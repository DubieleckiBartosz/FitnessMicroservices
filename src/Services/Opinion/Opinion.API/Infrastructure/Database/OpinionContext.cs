using Microsoft.EntityFrameworkCore;
using Opinion.API.Domain;

namespace Opinion.API.Infrastructure.Database;

public class OpinionContext : DbContext
{
    public DbSet<Domain.Opinion> Opinions { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public OpinionContext(DbContextOptions<OpinionContext> options) : base(options)
    {
    }
}
using Microsoft.EntityFrameworkCore;
using Prismo.Core.Features.Companies.Domain;

namespace Prismo.Core.Infrastructure.Persistence;

public class PrismoDbContext : DbContext
{
    public PrismoDbContext(DbContextOptions<PrismoDbContext> options) : base(options){}

    public DbSet<Company> Companies => Set<Company>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrismoDbContext).Assembly);
    }
}

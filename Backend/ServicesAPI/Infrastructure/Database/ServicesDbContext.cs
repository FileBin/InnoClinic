using ServicesAPI.Domain;

namespace ServicesAPI.Infrastructure;

internal class ServicesDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<Service> Services => Set<Service>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServicesDbContext).Assembly);
    }
}

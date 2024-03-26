using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using OfficesAPI.Domain.Models;

namespace OfficesAPI.Infrastructure.Database;

public class OfficesDbContext : DbContext {
    public OfficesDbContext(DbContextOptions options) : base(options) {
    }

    public DbSet<Office> Movies => Set<Office>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Office>().ToCollection("offices");
    }
}

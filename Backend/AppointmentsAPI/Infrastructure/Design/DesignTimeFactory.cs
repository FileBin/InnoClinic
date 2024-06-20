using AppointmentsAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AppointmentsAPI.Infrastructure.Design;

internal class TestContextFactory : IDesignTimeDbContextFactory<AppointmentsDbContext> {
    public AppointmentsDbContext CreateDbContext(string[] args) {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("design.secrets.json")
            .Build();
        var optionsBuilder = new DbContextOptionsBuilder<AppointmentsDbContext>();
        return new AppointmentsDbContext(optionsBuilder.Options, config);
    }
}
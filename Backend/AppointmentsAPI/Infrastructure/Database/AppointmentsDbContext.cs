using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AppointmentsAPI.Infrastructure.Database;

internal class AppointmentsDbContext(DbContextOptions options, IConfiguration config) : DbContext(options) {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);

        var dbSection = config.GetSection("AppointmentsDb");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder($"Host={dbSection.GetOrThrow("Host")};"
                                    + $"Port={dbSection.GetOrThrow("Port")};"
                                    + $"Username={dbSection.GetOrThrow("User")};"
                                    + $"Password={dbSection.GetOrThrow("Password")};"
                                    + $"Database={dbSection.GetOrThrow("Database")};");

        var dataSource = dataSourceBuilder.Build();

        var assembly = typeof(AppointmentsDbContext).Assembly;

        optionsBuilder.UseNpgsql(dataSource, x => x.MigrationsAssembly(assembly.FullName));
    }

    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentResult> AppointmentResults => Set<AppointmentResult>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Specialization> Specializations => Set<Specialization>();

}

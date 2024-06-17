using AppointmentsAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Infrastructure.Database;

internal class AppointmentsDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentResult> AppointmentResults => Set<AppointmentResult>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Specialization> Specializations => Set<Specialization>();

}

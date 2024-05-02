using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Domain.Models;

[Table("appointments")]
public class Appointment : IEntity {
    [Column("id")]
    public Guid Id { get; set; }

    [Column("patient_id")]
    public Guid PatientId { get; set; }

    [Column("doctor_id")]
    public Guid DoctorId { get; set; }

    [Column("service_id")]
    public Guid ServiceId { get; set; }

    [Column("date")]
    public required DateOnly Date { get; set; }

    [Column("time")]
    public required TimeOnly Time { get; set; }

    [Column("is_active")]
    public required bool IsApproved { get; set; }
}

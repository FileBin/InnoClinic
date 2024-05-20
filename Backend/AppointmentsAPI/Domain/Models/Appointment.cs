using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Domain.Models;

[Table("appointments")]
public class Appointment : IEntity {
    [Column("id")]
    public Guid Id { get; set; }

    [Column("patient_id")]
    public Guid PatientId { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient PatientProfile { get; set; } = null!;


    [Column("doctor_id")]
    public Guid DoctorId { get; set; }

    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor DoctorProfile { get; set; } = null!;

    [Column("service_id")]
    public Guid ServiceId { get; set; }

    [ForeignKey(nameof(ServiceId))]
    public virtual Service Service { get; set; } = null!;

    [Column("date")]
    public required DateOnly Date { get; set; }

    [Column("begin_time")]
    public required TimeOnly BeginTime { get; set; }

    [Column("end_time")]
    public required TimeOnly EndTime { get; set; }

    [Column("is_active")]
    public required bool IsApproved { get; set; }

    public virtual AppointmentResult? AppointmentResult { get; set; }
}

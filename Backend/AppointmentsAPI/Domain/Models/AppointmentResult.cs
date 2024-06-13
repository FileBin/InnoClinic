using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Domain.Models;

[Table("results")]
public class AppointmentResult : IEntity {
    [Column("id")]
    public Guid Id { get; set; }

    [Column("patient_id")]
    public Guid AppointmentId { get; set; }
    
    [ForeignKey(nameof(AppointmentId))]
    public Appointment Appointment { get; set; } = null!;

    [Column("complaints")]
    public required string Complaints { get; set; }

    [Column("conclusion")]
    public required string Conclusion { get; set; }

    [Column("recommendations")]
    public required string Recommendations { get; set; }

    [Column("is_finished")]
    public bool IsFinished { get; set; }
}

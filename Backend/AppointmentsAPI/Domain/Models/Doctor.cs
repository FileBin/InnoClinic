using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Domain.Models;

[Table("doctors")]
public class Doctor : IEntity {
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }

    public Guid OfficeId { get; set; }
}

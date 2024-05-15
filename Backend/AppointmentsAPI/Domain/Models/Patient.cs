using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Domain.Models;

[Table("patients")]
public class Patient : IEntity {
    [Column("id")]
    public Guid Id { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }

    public DateOnly DateOfBirth { get; set; }
}
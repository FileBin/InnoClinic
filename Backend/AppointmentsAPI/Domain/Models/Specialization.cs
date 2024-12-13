using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Domain.Models;

[Table("specializations")]
public class Specialization : INamedEntity {

    [Column("id")]
    public Guid Id { get; set; }

    [Column("specialization_name")]
    public required string Name { get; set; }

    public virtual ICollection<Service> Services { get; set; } = null!;
}

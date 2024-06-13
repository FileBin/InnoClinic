using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Domain.Models;

[Table("services")]
public class Service : IEntity {
    [Column("id")]
    public Guid Id { get; set; }

    [Column("service_name")]
    public required string Name { get; set; }

    [Column("specialization_id")]
    public Guid SpecializationId { get; set; }

    [ForeignKey(nameof(SpecializationId))]
    public virtual Specialization Specialization { get; set; } = null!;
}

namespace ServicesAPI.Domain;

[Table("services")]
public class Service : IEntity {
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("specialization_id")]
    public Guid SpecializationId { get; set; }

    [ForeignKey(nameof(SpecializationId))]
    public virtual Specialization Specialization { get; set; } = null!;

    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual ServiceCategory Category { get; set; } = null!;

    [Column("service_name")]
    public required string Name { get; set; }

    [Column("price")]
    public required decimal Price { get; set; }

    [Column("isActive")]
    public required bool IsActive { get; set; }
}

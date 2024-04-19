namespace ServicesAPI.Domain;

[Table("services")]
public class Service {
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("specialization_id")]
    public Guid SpecializationId { get; set; }

    [ForeignKey(nameof(SpecializationId))]
    public virtual required Specialization Specialization { get; set; }

    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual required ServiceCategory Category { get; set; }

    [Column("service_name")]
    public required string Name { get; set; }

    [Column("price")]
    public required decimal Price { get; set; }

    [Column("isActive")]
    public required bool IsActive { get; set; }
}

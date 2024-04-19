namespace ServicesAPI.Domain;

[Table("specializations")]
public class Specialization {
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("specialization_name")]
    public required string Name { get; set; }

    [Column("isActive")]
    public required bool IsActive { get; set; }
    
    public virtual ICollection<Service> Services { get; set; } = null!;
}

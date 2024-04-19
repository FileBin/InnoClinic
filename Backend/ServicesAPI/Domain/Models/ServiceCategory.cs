namespace ServicesAPI.Domain;

[Table("service_categories")]
public class ServiceCategory {
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("category_name")]
    public required string Name { get; set; }

    [Column("time_slot_size")]
    public required int TimeSlotSize { get; set; }

    public virtual ICollection<Service> Services { get; set; } = null!;
}

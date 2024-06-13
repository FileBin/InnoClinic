using InnoClinic.Shared.Misc.Repository;
using ServicesAPI.Domain;

namespace ServicesAPI.Infrastructure.Repository;

internal class SpecializationsRepository(ServicesDbContext dbContext) : CrudRepositoryBase<Specialization> {
    protected override DbSet<Specialization> GetDbSet() => dbContext.Specializations;
}

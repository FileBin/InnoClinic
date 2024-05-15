using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc;
using InnoClinic.Shared.Misc.Repository;
using Mapster;
using OfficesAPI.Application.Contracts.Models.Requests;
using OfficesAPI.Application.Contracts.Models.Responses;
using OfficesAPI.Application.Contracts.Services;
using OfficesAPI.Domain.Models;

namespace OfficesAPI.Application.Services;

internal class OfficeService(IRepository<Office> officeRepository, IUnitOfWork unitOfWork) : IOfficeService {
    public async Task<OfficeResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        var office = await officeRepository.GetByIdOrThrow(id, cancellationToken);
        return office.Adapt<OfficeResponse>();
    }

    public async Task<IEnumerable<OfficeResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        var offices = await officeRepository.GetPageAsync(pageDesc, cancellationToken);
        return offices.Select(o => o.Adapt<OfficeResponse>()).ToList();
    }

    public async Task<Guid> CreateAsync(OfficeCreateRequest createDto, CancellationToken cancellationToken = default) {
        var office = createDto.Adapt<Office>();
        officeRepository.Create(office);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return office.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        await officeRepository.DeleteByIdAsync(id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid id, OfficeUpdateRequest updateDto, CancellationToken cancellationToken = default) {
        var office = await officeRepository.GetByIdOrThrow(id, cancellationToken);
        updateDto.Adapt(office);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

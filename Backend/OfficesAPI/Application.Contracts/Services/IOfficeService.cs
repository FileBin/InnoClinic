using OfficesAPI.Application.Contracts.Models.Requests;
using OfficesAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Domain.Abstractions;

namespace OfficesAPI.Application.Contracts.Services;

public interface IOfficeService {
    Task<OfficeResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<OfficeResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(OfficeCreateRequest createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, OfficeUpdateRequest updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
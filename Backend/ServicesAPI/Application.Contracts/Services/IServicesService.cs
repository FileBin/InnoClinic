using InnoClinic.Shared.Domain.Abstractions;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;

namespace ServicesAPI.Application.Contracts.Services;

public interface IServicesService {
    Task<ServiceResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ServiceResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(ServiceCreateRequest createRequest, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, ServiceUpdateRequest updateRequest, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

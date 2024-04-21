using InnoClinic.Shared.Domain.Abstractions;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;

namespace ServicesAPI.Application.Contracts.Services;

public interface IServiceCategoriesService {
    Task<ServiceCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ServiceResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(ServiceCategoryCreateRequest createRequest, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, ServiceCategoryUpdateRequest updateRequest, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

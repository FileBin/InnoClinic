using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using Mapster;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;
using ServicesAPI.Application.Contracts.Services;
using ServicesAPI.Domain;

namespace ServicesAPI.Application;

internal class ServiceCategoriesService(
    IRepository<ServiceCategory> categoryRepository,
    IUnitOfWork unitOfWork) : IServiceCategoriesService {
    public async Task<ServiceCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        var specialization = await categoryRepository.GetByIdAsync(id, cancellationToken);

        if (specialization is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(specialization));
        }

        return specialization.Adapt<ServiceCategoryResponse>();
    }

    public async Task<IEnumerable<ServiceCategoryResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        var services = await categoryRepository.GetPageAsync(pageDesc, cancellationToken);

        return services.Select(o => o.Adapt<ServiceCategoryResponse>()).ToList();
    }

    public async Task<Guid> CreateAsync(ServiceCategoryCreateRequest createRequest, CancellationToken cancellationToken = default) {
        var specialization = createRequest.Adapt<ServiceCategory>();

        categoryRepository.Create(specialization);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return specialization.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var specialization = await categoryRepository.GetByIdAsync(id, cancellationToken);

        if (specialization is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(specialization));
        }

        categoryRepository.Delete(specialization);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public async Task UpdateAsync(Guid id, ServiceCategoryUpdateRequest updateRequest, CancellationToken cancellationToken = default) {
        var specialization = await categoryRepository.GetByIdAsync(id, cancellationToken);

        if (specialization is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(specialization));
        }

        if (updateRequest.TimeSlotSize.HasValue) {
            specialization.TimeSlotSize = updateRequest.TimeSlotSize.Value;
        }

        if (updateRequest.Name is not null) {
            specialization.Name = updateRequest.Name;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

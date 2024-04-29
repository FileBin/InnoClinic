using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using InnoClinic.Shared.Misc;
using Mapster;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;
using ServicesAPI.Application.Contracts.Services;
using ServicesAPI.Domain;

namespace ServicesAPI.Application;

internal class ServiceCategoriesService(
    IRepository<ServiceCategory> categoryRepository,
    IValidator<ServiceCategory> categoryValidator,
    IUnitOfWork unitOfWork) : IServiceCategoriesService {
    public async Task<ServiceCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(category));
        }

        return category.Adapt<ServiceCategoryResponse>();
    }

    public async Task<IEnumerable<ServiceCategoryResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        var services = await categoryRepository.GetPageAsync(pageDesc, cancellationToken);

        return services.Select(o => o.Adapt<ServiceCategoryResponse>()).ToList();
    }

    public async Task<Guid> CreateAsync(ServiceCategoryCreateRequest createRequest, CancellationToken cancellationToken = default) {
        var category = createRequest.Adapt<ServiceCategory>();

        await categoryValidator.ValidateAndThrowAsync(category, cancellationToken);


        categoryRepository.Create(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(category));
        }

        categoryRepository.Delete(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public async Task UpdateAsync(Guid id, ServiceCategoryUpdateRequest updateRequest, CancellationToken cancellationToken = default) {
        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);

        using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        if (category is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(category));
        }

        if (updateRequest.TimeSlotSize.HasValue) {
            category.TimeSlotSize = updateRequest.TimeSlotSize.Value;
        }

        if (updateRequest.Name is not null) {
            category.Name = updateRequest.Name;
        }

        try {
            await categoryValidator.ValidateAndThrowAsync(category, cancellationToken);
        } catch (ValidationException) {
            await transaction.RollbackAsync(cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}

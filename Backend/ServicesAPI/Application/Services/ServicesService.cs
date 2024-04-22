using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using Mapster;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;
using ServicesAPI.Application.Contracts.Services;
using ServicesAPI.Application.Helpers;
using ServicesAPI.Domain;

namespace ServicesAPI.Application;

internal class ServicesService(
    IRepository<Service> servicesRepository,
    IRepository<ServiceCategory> categoryRepository,
    IRepository<Specialization> specializationRepository,
    IValidator<Service> serviceValidator,
    IUnitOfWork unitOfWork) : IServicesService {

    public async Task<ServiceResponse> GetByIdAsync(Guid id, IUserDescriptor userDesc, CancellationToken cancellationToken = default) {
        var service = await servicesRepository.GetByIdAsync(id, cancellationToken);

        if (service is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(service));
        }

        return service.Adapt<ServiceResponse>();
    }

    public async Task<IEnumerable<ServiceResponse>> GetPageAsync(IPageDesc pageDesc, IUserDescriptor userDesc, CancellationToken cancellationToken = default) {
        var services = await servicesRepository.GetAuthorizedPage(pageDesc, userDesc, cancellationToken);

        return services.Select(o => o.Adapt<ServiceResponse>()).ToList();
    }
    public async Task<Guid> CreateAsync(ServiceCreateRequest createRequest, CancellationToken cancellationToken = default) {      
        var service = createRequest.Adapt<Service>();

        await serviceValidator.ValidateAndThrowAsync(service, cancellationToken);

        var category = await categoryRepository.GetByIdAsync(service.CategoryId, cancellationToken);

        if (category is null) {
            throw new NotFoundException($"Category with id {service.CategoryId} not found");
        }

        var specialization = await specializationRepository.GetByIdAsync(service.SpecializationId, cancellationToken);

        if (specialization is null) {
            throw new NotFoundException($"Specialization with id {service.SpecializationId} not found");
        }

        servicesRepository.Create(service);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return service.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var service = await servicesRepository.GetByIdAsync(id, cancellationToken);

        if (service is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(service));
        }

        servicesRepository.Delete(service);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public async Task UpdateAsync(Guid id, ServiceUpdateRequest updateRequest, CancellationToken cancellationToken = default) {
        var service = await servicesRepository.GetByIdAsync(id, cancellationToken);

        if (service is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(service));
        }

        if (updateRequest.CategoryId.HasValue) {
            var category = await categoryRepository.GetByIdAsync(updateRequest.CategoryId.Value, cancellationToken)
                ?? throw new NotFoundException($"Category with id {updateRequest.CategoryId} not found");
            service.Category = category;
        }

        if (updateRequest.SpecializationId.HasValue) {
            var specialization = await specializationRepository.GetByIdAsync(updateRequest.SpecializationId.Value, cancellationToken) ?? throw new NotFoundException($"Specialization with id {service.SpecializationId} not found");
            service.Specialization = specialization;
        }

        if (updateRequest.Price.HasValue) {
            service.Price = updateRequest.Price.Value;
        }

        if (updateRequest.IsActive.HasValue) {
            service.IsActive = updateRequest.IsActive.Value;
        }

        if (updateRequest.Name is not null) {
            service.Name = updateRequest.Name;
        }

        await serviceValidator.ValidateAndThrowAsync(service, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

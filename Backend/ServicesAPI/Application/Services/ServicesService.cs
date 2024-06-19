using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using Mapster;
using InnoClinic.Shared.Misc.Repository;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;
using ServicesAPI.Application.Contracts.Services;
using ServicesAPI.Application.Helpers;
using ServicesAPI.Domain;
using MassTransit;
using InnoClinic.Shared.Messaging.Contracts.Models.Service;
using InnoClinic.Shared.Messaging.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace ServicesAPI.Application.Services;

internal class ServicesService(
    IRepository<Service> servicesRepository,
    IRepository<ServiceCategory> categoryRepository,
    IRepository<Specialization> specializationRepository,
    IValidator<Service> serviceValidator,
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint) : IServicesService {

    public async Task<ServiceResponse> GetByIdAsync(Guid id, IUserDescriptor userDesc, CancellationToken cancellationToken = default) {
        var service = await servicesRepository
            .GetAll()
            .Include(x => x.Specialization)
            .Include(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken) 
            ?? throw new NotFoundException($"{nameof(Service)} with id {id} was not found");
            
        service.ValidateVisibility(userDesc);

        return service.Adapt<ServiceResponse>();
    }

    public async Task<IEnumerable<ServiceResponse>> GetPageAsync(IPageDesc pageDesc, IUserDescriptor userDesc, CancellationToken cancellationToken = default) {
        var services = await servicesRepository.GetAuthorizedPage(pageDesc, userDesc, cancellationToken);

        return services.Select(o => o.Adapt<ServiceResponse>()).ToList();
    }

    public async Task<Guid> CreateAsync(ServiceCreateRequest createRequest, CancellationToken cancellationToken = default) {
        var service = createRequest.Adapt<Service>();

        await serviceValidator.ValidateAndThrowAsync(service, cancellationToken);

        await categoryRepository.EnsureExistsAsync(service.CategoryId, cancellationToken);
        await specializationRepository.EnsureExistsAsync(service.SpecializationId, cancellationToken);

        servicesRepository.Create(service);

        await publishEndpoint.Publish(service.Adapt<ServiceCreated>(), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return service.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var service = await servicesRepository.GetByIdOrThrow(id, cancellationToken);

        servicesRepository.Delete(service);

        await publishEndpoint.Publish(service.Adapt<ServiceDeleted>(), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public async Task UpdateAsync(Guid id, ServiceUpdateRequest updateRequest, CancellationToken cancellationToken = default) {
        var service = await servicesRepository.GetByIdAsync(id, cancellationToken);

        using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        if (service is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(service));
        }

        if (updateRequest.CategoryId.HasValue)
            await categoryRepository.EnsureExistsAsync(updateRequest.CategoryId.Value, cancellationToken);
        if (updateRequest.SpecializationId.HasValue)
            await specializationRepository.EnsureExistsAsync(updateRequest.SpecializationId.Value, cancellationToken);

        updateRequest.Adapt(service);

        try {
            await serviceValidator.ValidateAndThrowAsync(service, cancellationToken);
        } catch (ValidationException) {
            await transaction.RollbackAsync(cancellationToken);
        }

        await publishEndpoint.Publish(service.Adapt<ServiceUpdated>(), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }
}

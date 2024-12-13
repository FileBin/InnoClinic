using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using InnoClinic.Shared.Messaging.Contracts.Models.Specialization;
using InnoClinic.Shared.Misc;
using InnoClinic.Shared.Misc.Repository;
using Mapster;
using MassTransit;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;
using ServicesAPI.Application.Contracts.Services;
using ServicesAPI.Domain;

namespace ServicesAPI.Application;

internal class SpecializationsService(
    IRepository<Specialization> specializationRepository,
    IValidator<Specialization> specializationValidator,
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint) : ISpecializationsService {
    public async Task<SpecializationResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        var specialization = await specializationRepository.GetByIdOrThrow(id, cancellationToken);

        return specialization.Adapt<SpecializationResponse>();
    }

    public async Task<IEnumerable<SpecializationResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        var services = await specializationRepository.GetPageAsync(pageDesc, cancellationToken);

        return services.Select(o => o.Adapt<SpecializationResponse>()).ToList();
    }

    public async Task<Guid> CreateAsync(SpecializationCreateRequest createRequest, CancellationToken cancellationToken = default) {
        var specialization = createRequest.Adapt<Specialization>();

        await specializationValidator.ValidateAndThrowAsync(specialization, cancellationToken);

        specializationRepository.Create(specialization);

        await publishEndpoint.Publish(specialization.Adapt<SpecializationCreated>(), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return specialization.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var specialization = await specializationRepository.GetByIdOrThrow(id, cancellationToken);

        specializationRepository.Delete(specialization);

        await publishEndpoint.Publish(specialization.Adapt<SpecializationDeleted>(), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public async Task UpdateAsync(Guid id, SpecializationUpdateRequest updateRequest, CancellationToken cancellationToken = default) {
        var specialization = await specializationRepository.GetByIdOrThrow(id, cancellationToken);

        var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        updateRequest.Adapt(specialization);

        try {
            await specializationValidator.ValidateAndThrowAsync(specialization, cancellationToken);
        } catch (ValidationException) {
            await transaction.RollbackAsync(cancellationToken);
        }

        await publishEndpoint.Publish(specialization.Adapt<SpecializationUpdated>(), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }
}

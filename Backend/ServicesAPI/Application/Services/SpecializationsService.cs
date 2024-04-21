using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using Mapster;
using ServicesAPI.Application.Contracts.Models.Requests;
using ServicesAPI.Application.Contracts.Models.Responses;
using ServicesAPI.Application.Contracts.Services;
using ServicesAPI.Domain;

namespace ServicesAPI.Application;

internal class SpecializationsService(
    IRepository<Specialization> specializationRepository,
    IUnitOfWork unitOfWork) : ISpecializationsService {
    public async Task<SpecializationResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        var specialization = await specializationRepository.GetByIdAsync(id, cancellationToken);

        if (specialization is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(specialization));
        }

        return specialization.Adapt<SpecializationResponse>();
    }

    public async Task<IEnumerable<SpecializationResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        var services = await specializationRepository.GetPageAsync(pageDesc, cancellationToken);

        return services.Select(o => o.Adapt<SpecializationResponse>()).ToList();
    }

    public async Task<Guid> CreateAsync(SpecializationCreateRequest createRequest, CancellationToken cancellationToken = default) {
        var specialization = createRequest.Adapt<Specialization>();

        specializationRepository.Create(specialization);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return specialization.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var specialization = await specializationRepository.GetByIdAsync(id, cancellationToken);

        if (specialization is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(specialization));
        }

        specializationRepository.Delete(specialization);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


    public async Task UpdateAsync(Guid id, SpecializationUpdateRequest updateRequest, CancellationToken cancellationToken = default) {
        var specialization = await specializationRepository.GetByIdAsync(id, cancellationToken);

        if (specialization is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(specialization));
        }

        if (updateRequest.IsActive.HasValue) {
            specialization.IsActive = updateRequest.IsActive.Value;
        }

        if (updateRequest.Name is not null) {
            specialization.Name = updateRequest.Name;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

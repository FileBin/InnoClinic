﻿using Mapster;
using OfficesAPI.Application.Contracts.Models;
using OfficesAPI.Application.Contracts.Models.Requests;
using OfficesAPI.Application.Contracts.Models.Responses;
using OfficesAPI.Application.Contracts.Services;
using OfficesAPI.Domain.Models;
using Shared.Domain.Abstractions;
using Shared.Exceptions.Models;

namespace OfficesAPI.Application.Services;

internal class OfficeService(IRepository<Office> officeRepository, IUnitOfWork unitOfWork) : IOfficeService {
    public async Task<OfficeResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        var office = await officeRepository.GetByIdAsync(id, cancellationToken);

        if (office is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(office));
        }

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
        var office = await officeRepository.GetByIdAsync(id, cancellationToken);

        if (office is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(office));
        }

        officeRepository.Delete(office);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid id, OfficeUpdateRequest updateDto, CancellationToken cancellationToken = default) {
        var office = await officeRepository.GetByIdAsync(id, cancellationToken);

        if (office is null) {
            throw NotFoundException.NotFoundInDatabase(nameof(office));
        }

        if (updateDto.Address is not null) {
            office.Address = updateDto.Address.Adapt<Address>();
        }

        if (! string.IsNullOrEmpty(updateDto.RegistryPhoneNumber)) {
            office.RegistryPhoneNumber = updateDto.RegistryPhoneNumber;
        }

        if (updateDto.IsActive.HasValue) {
            office.IsActive = updateDto.IsActive.Value;
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

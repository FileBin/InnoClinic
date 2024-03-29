using OfficesAPI.Application.Contracts.Models;
using Shared.Domain.Abstractions;

namespace OfficesAPI.Application.Contracts.Services;

public interface IOfficeService {
    Task<OfficeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<OfficeDto>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(OfficeCreateDto createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, OfficeUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
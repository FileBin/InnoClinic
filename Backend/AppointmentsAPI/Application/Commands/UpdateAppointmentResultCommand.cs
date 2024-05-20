using AppointmentsAPI.Application.Contracts.Commands;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Commands;

public record UpdateAppointmentResultCommand : ICommand {

    public required IUserDescriptor UserDescriptor { get; init; }

    public Guid AppointmentId { get; init; }

    public string? Complaints { get; init; }
    public string? Conclusion { get; init; }
    public string? Recommendations { get; init; }

    public bool? IsFinished { get; init; }
}
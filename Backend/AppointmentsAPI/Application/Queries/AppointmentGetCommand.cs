using AppointmentsAPI.Application.Contracts.Commands;
using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Application.Queries;

public record AppointmentGetCommand : ICommand<AppointmentResponse> {
    public Guid AppointmentId { get; init; }
}

using AppointmentsAPI.Application.Contracts.Commands;
using AppointmentsAPI.Application.Contracts.Models.Requests;

namespace AppointmentsAPI.Application;

public record AppointmentCreateCommand : AppointmentCreateRequest, ICommand<Guid> {}

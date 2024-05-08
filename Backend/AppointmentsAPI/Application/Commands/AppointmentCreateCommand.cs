using AppointmentsAPI.Application.Contracts.Commands;
using AppointmentsAPI.Application.Contracts.Models.Requests;

namespace AppointmentsAPI.Application.Commands;

public record AppointmentCreateCommand : AppointmentCreateRequest, ICommand<Guid> { }

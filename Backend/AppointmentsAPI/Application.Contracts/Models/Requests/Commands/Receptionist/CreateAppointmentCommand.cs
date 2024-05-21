namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;

public record CreateAppointmentCommand : Common.CreateAppointmentRequest, IRequest<Guid>;

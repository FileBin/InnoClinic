namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;

public record CancelAppointmentCommand : Common.CancelAppointmentRequest, IRequest;

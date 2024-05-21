namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;

public record ApproveAppointmentCommand : Common.ApproveAppointmentRequest, IRequest;
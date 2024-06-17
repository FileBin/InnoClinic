namespace AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;

public record RescheduleAppointmentCommand : Common.RescheduleAppointmentRequest, IRequest;

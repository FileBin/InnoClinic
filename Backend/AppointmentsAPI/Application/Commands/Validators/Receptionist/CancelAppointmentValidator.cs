using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators.Receptionist;

public class CancelAppointmentValidator : AbstractValidator<CancelAppointmentCommand> {
    public CancelAppointmentValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}
using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators;

public class CancelAppointmentValidator : AbstractValidator<CancelAppointmentCommand> {
    public CancelAppointmentValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}
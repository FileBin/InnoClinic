using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators;

public class AppointmentApproveValidator : AbstractValidator<AppointmentApproveCommand> {
    public AppointmentApproveValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}

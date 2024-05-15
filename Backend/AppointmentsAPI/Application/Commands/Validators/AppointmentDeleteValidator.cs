using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators;

public class AppointmentDeleteValidator : AbstractValidator<AppointmentDeleteCommand> {
    public AppointmentDeleteValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}
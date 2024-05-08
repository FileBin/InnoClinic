using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators;

public class AppointmentUpdateValidator : AbstractValidator<AppointmentUpdateCommand> {
    public AppointmentUpdateValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}

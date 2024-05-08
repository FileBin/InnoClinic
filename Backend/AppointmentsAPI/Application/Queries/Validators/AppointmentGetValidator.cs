using FluentValidation;

namespace AppointmentsAPI.Application.Queries.Validators;

public class AppointmentGetValidator : AbstractValidator<AppointmentGetCommand> {

        public AppointmentGetValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}

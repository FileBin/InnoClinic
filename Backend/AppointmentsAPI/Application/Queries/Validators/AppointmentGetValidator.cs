using FluentValidation;

namespace AppointmentsAPI.Application.Queries.Validators;

public class AppointmentGetValidator : AbstractValidator<AppointmentGetQuery> {

        public AppointmentGetValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}

using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators;

public class CreateAppointmentResultValidator : AbstractValidator<CreateAppointmentResultCommand> {
    public CreateAppointmentResultValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}

using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators.Patient;

public class CancelAppointmentValidator : AbstractValidator<CancelAppointmentCommand> {
    public CancelAppointmentValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}
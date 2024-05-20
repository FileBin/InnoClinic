using AppointmentsAPI.Application.Contracts.Models.Requests.Commands;
using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators;

public class ApproveAppointmentValidator : AbstractValidator<ApproveAppointmentCommand> {
    public ApproveAppointmentValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}

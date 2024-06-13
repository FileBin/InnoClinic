using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators;

public class ApproveAppointmentValidator : AbstractValidator<ApproveAppointmentCommand> {
    public ApproveAppointmentValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}

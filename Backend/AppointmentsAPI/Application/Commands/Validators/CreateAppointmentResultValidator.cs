using AppointmentsAPI.Domain.Models;
using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Commands.Validators;

public class CreateAppointmentResultValidator : AbstractValidator<CreateAppointmentResultCommand> {
    public CreateAppointmentResultValidator(IRepository<Appointment> repository) {
        RuleFor(x => x.AppointmentId).ValidateAppointmentId(repository);
    }
}

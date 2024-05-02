using FluentValidation;

namespace AppointmentsAPI.Application.Contracts;

public class AppointmentCreateValidator : AbstractValidator<AppointmentCreateCommand> {
    public AppointmentCreateValidator() {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.ServiceId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
    }
}

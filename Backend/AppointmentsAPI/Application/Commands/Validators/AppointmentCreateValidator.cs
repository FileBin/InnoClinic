using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Services;
using FluentValidation;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Validators;

public class AppointmentCreateValidator : AbstractValidator<AppointmentCreateCommand> {
    public AppointmentCreateValidator(ITimeSlotService timeSlotService) {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.ServiceId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();

        RuleFor(x => x.Adapt<TimeSlotRequest>())
            .MustAsync(timeSlotService.VerifyTimeSlot)
            .WithMessage(x => $"Given TimeSlot ({x}) is invalid");

    }
}

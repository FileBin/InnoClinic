using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Application.Contracts.Services;
using FluentValidation;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Validators.Patient;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand> {
    public CreateAppointmentValidator(ITimeSlotService timeSlotService) {
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.ServiceId).NotEmpty();

        RuleFor(x => x.Adapt<TimeSlotRequest>()).ValidateTimeSlot(timeSlotService);
    }
}

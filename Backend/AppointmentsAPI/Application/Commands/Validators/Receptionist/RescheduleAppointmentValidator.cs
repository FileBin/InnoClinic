using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Application.Contracts.Services;
using FluentValidation;

namespace AppointmentsAPI.Application.Commands.Validators.Receptionist;

public class RescheduleAppointmentValidator : AbstractValidator<RescheduleAppointmentCommand> {
    public RescheduleAppointmentValidator(ITimeSlotService timeSlotService) {
        RuleFor(x => x.AppointmentId).NotEmpty();
        RuleFor(x => x as TimeSlotRequest).ValidateTimeSlot(timeSlotService);
    }
}

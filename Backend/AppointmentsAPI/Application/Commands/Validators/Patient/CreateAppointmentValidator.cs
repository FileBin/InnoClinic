using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Application.Contracts.Services;
using AppointmentsAPI.Domain.Models;
using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Validators.Patient;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand> {
    public CreateAppointmentValidator(ITimeSlotService timeSlotService,
    IRepository<Doctor> doctorRepo, 
    IRepository<Service> serviceRepo) {

        RuleFor(x => x.DoctorId).NotEmpty().ValidateEntity(doctorRepo);
        RuleFor(x => x.ServiceId).NotEmpty().ValidateEntity(serviceRepo);

        RuleFor(x => x.Adapt<TimeSlotRequest>()).ValidateTimeSlot(timeSlotService);
    }
}

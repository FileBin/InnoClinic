using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Application.Contracts.Services;
using AppointmentsAPI.Domain.Models;
using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Validators.Receptionist;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand> {
    public CreateAppointmentValidator(ITimeSlotService timeSlotService,
    IRepository<Doctor> doctorRepo, 
    IRepository<Service> serviceRepo,
    IRepository<Domain.Models.Patient> patientRepo) {
        RuleFor(x => x.DoctorId).NotEmpty().ValidateEntity(doctorRepo);
        RuleFor(x => x.ServiceId).NotEmpty().ValidateEntity(serviceRepo);
        RuleFor(x => x.PatientId).NotEmpty().ValidateEntity(patientRepo);

        RuleFor(x => x.Adapt<TimeSlotRequest>()).ValidateTimeSlot(timeSlotService);
    }
}

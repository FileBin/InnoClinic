using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Abstraction;

public interface IDoctorRequest {
    IUserDescriptor DoctorDescriptor { get; }

}
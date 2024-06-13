using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application.Contracts.Abstraction;

public interface IPatientRequest {
    IUserDescriptor PatientDescriptor { get; }
}

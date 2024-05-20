using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Domain.Models;

namespace AppointmentsAPI.Application.Contracts.Services;

public interface ITimeSlotService {
    public Task<bool> VerifyTimeSlot(TimeSlotRequest timeSlot, CancellationToken cancellationToken = default);
    public Task<IEnumerable<TimeSlotResponse>> GetBusySlots(DateOnly date, CancellationToken cancellationToken = default);
}

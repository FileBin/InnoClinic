using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Application.Contracts.Services;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Application.Services;

public class TimeSlotService(IRepository<Appointment> appointmentRepo) : ITimeSlotService {
    public async Task<IEnumerable<TimeSlotResponse>> GetBusySlots(DateOnly date, CancellationToken cancellationToken = default) {
        var appointments = await appointmentRepo.GetAll().Where(a => a.Date == date).OrderBy(a => a.BeginTime).ToListAsync(cancellationToken);
        return appointments.Adapt<List<TimeSlotResponse>>();
    }

    public async Task<bool> VerifyTimeSlot(TimeSlotRequest timeSlot, CancellationToken cancellationToken = default) {
        if(timeSlot.Date < DateOnly.FromDateTime(DateTime.Now)) return false;
        
        if (timeSlot.BeginTime >= timeSlot.EndTime) return false;

        var timeSlots = await GetBusySlots(timeSlot.Date, cancellationToken);

        foreach (var otherSlot in timeSlots) {
            if (timeSlot.BeginTime > otherSlot.EndTime) continue;
            if (otherSlot.BeginTime > timeSlot.EndTime) continue;

            return false;
        }
        return true;
    }
}
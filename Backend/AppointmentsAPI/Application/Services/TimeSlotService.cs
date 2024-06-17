using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using AppointmentsAPI.Application.Contracts.Services;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AppointmentsAPI.Application.Services;

public class TimeSlotService(IRepository<Appointment> appointmentRepo, IConfiguration config) : ITimeSlotService {
    public async Task<IEnumerable<TimeSlotResponse>> GetBusySlots(DateOnly date, CancellationToken cancellationToken = default) {
        var appointments = await appointmentRepo.GetAll().Where(a => a.Date == date).OrderBy(a => a.BeginTime).ToListAsync(cancellationToken);
        return appointments.Adapt<List<TimeSlotResponse>>();
    }

    public async Task<bool> VerifyTimeSlot(TimeSlotRequest timeSlot, CancellationToken cancellationToken = default) {
        if (timeSlot.Date < DateOnly.FromDateTime(DateTime.Now)) return false;
        if (timeSlot.BeginTime >= timeSlot.EndTime) return false;

        if (!double.TryParse(config["TimeSlots::MaxDays"], out var maxDays))
            maxDays = Defaults.DefaultMaxDays;
        if (timeSlot.Date.ToDateTime(TimeOnly.MinValue) - DateTime.Today > TimeSpan.FromDays(maxDays)) return false;

        if (!double.TryParse(config["TimeSlots::MaxMinutesLength"], out var maxMinutes))
            maxMinutes = Defaults.DefaultMaxMinutes;
        if (timeSlot.EndTime - timeSlot.BeginTime > TimeSpan.FromMinutes(maxMinutes)) return false;

        if (!double.TryParse(config["TimeSlots::MinMinutesLength"], out var minMinutes))
            minMinutes = Defaults.DefaultMinMinutes;
        if (timeSlot.EndTime - timeSlot.BeginTime < TimeSpan.FromMinutes(minMinutes)) return false;

        var timeSlots = await GetBusySlots(timeSlot.Date, cancellationToken);

        foreach (var otherSlot in timeSlots) {
            if(timeSlot.BeginTime - otherSlot.EndTime > TimeSpan.FromMinutes(minMinutes)) continue;
            if(otherSlot.BeginTime - timeSlot.EndTime > TimeSpan.FromMinutes(minMinutes)) continue;

            return false;
        }
        return true;
    }
}
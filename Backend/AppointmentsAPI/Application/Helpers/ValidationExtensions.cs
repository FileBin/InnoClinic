using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Services;
using FluentValidation;

namespace AppointmentsAPI.Application;

internal static class ValidationExtensions {
    public static IRuleBuilderOptions<T, TimeSlotRequest> ValidateTimeSlot<T>(this IRuleBuilder<T, TimeSlotRequest> ruleBuilder, ITimeSlotService timeSlotService) {
        return ruleBuilder.MustAsync(timeSlotService.VerifyTimeSlot)
            .WithMessage(x => $"Given TimeSlot ({x}) is invalid");
    }
}

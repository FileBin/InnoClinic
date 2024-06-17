using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Services;
using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;

namespace AppointmentsAPI.Application;

internal static class ValidationExtensions {
    public static IRuleBuilderOptions<T, TimeSlotRequest> ValidateTimeSlot<T>(this IRuleBuilder<T, TimeSlotRequest> ruleBuilder, ITimeSlotService timeSlotService) {
        return ruleBuilder.MustAsync(timeSlotService.VerifyTimeSlot)
            .WithMessage(x => $"Given TimeSlot ({x}) is invalid");
    }

    public static IRuleBuilderOptions<T, Guid> ValidateEntity<T, E>(this IRuleBuilder<T, Guid> ruleBuilder, IRepository<E> repository) where E : IEntity {
        return ruleBuilder.MustAsync(async (id, ct) => await repository.GetByIdAsync(id, ct) is not null)
            .WithMessage(x => $"Given TimeSlot ({x}) does not exist");
    }
}

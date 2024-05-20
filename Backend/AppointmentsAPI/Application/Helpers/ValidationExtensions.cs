using AppointmentsAPI.Domain.Models;
using FluentValidation;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;

namespace AppointmentsAPI.Application;

internal static class ValidationExtensions {
    public static IRuleBuilderOptions<T, Guid> ValidateAppointmentId<T>(this IRuleBuilder<T, Guid> ruleBuilder, IRepository<Appointment> repository) {
        return ruleBuilder.NotEmpty().MustAsync(async (id, ct) => await repository.ExistsAsync(id, ct))
            .WithMessage(id => $"Appointment with id {id} does not exist in repository");
    }
}

using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models.Specialization;
using InnoClinic.Shared.Misc.Repository;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Specialization;

using Specialization = Domain.Models.Specialization;

public class SpecializationDeletedConsumer(ILogger<SpecializationDeletedConsumer> logger, IRepository<Specialization> SpecializationRepo, IUnitOfWork unitOfWork) : IConsumer<SpecializationDeleted> {
    public async Task Consume(ConsumeContext<SpecializationDeleted> context) {
        logger.LogInformation("SpecializationDeleted Message received with Id={MessageId}", context.MessageId);

        var specialization = await SpecializationRepo.GetByIdAsync(context.Message.Id, context.CancellationToken);

        if (specialization == null) {
            logger.LogWarning("Specialization with Id={Id} was not found", context.Message.Id);
            return;
        }
        SpecializationRepo.Delete(specialization);
        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("Specialization deleted with Id={Id} Name={Name}", specialization.Id, specialization.Name);
    }
}
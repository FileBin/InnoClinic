using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models;
using InnoClinic.Shared.Messaging.Contracts.Models.Specialization;
using InnoClinic.Shared.Misc.Repository;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Specialization;

using Specialization = Domain.Models.Specialization;

public class SpecializationUpdatedConsumer(ILogger<SpecializationUpdatedConsumer> logger, IRepository<Specialization> SpecializationRepo, IUnitOfWork unitOfWork) : IConsumer<SpecializationUpdated> {
    public async Task Consume(ConsumeContext<SpecializationUpdated> context) {
        logger.LogInformation("SpecializationUpdated Message received with Id={MessageId}", context.MessageId);

        var Specialization = await SpecializationRepo.GetByIdOrThrow(context.Message.Id, context.CancellationToken);
        context.Message.Adapt(Specialization);
        await unitOfWork.SaveChangesAsync();
        
        logger.LogInformation("Specialization updated with Id={Id} Name={Name}", Specialization.Id, Specialization.Name);
    }
}

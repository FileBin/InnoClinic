using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models.Specialization;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Specialization;

using Specialization = Domain.Models.Specialization;

public class SpecializationCreatedConsumer(ILogger<SpecializationCreatedConsumer> logger, IRepository<Specialization> SpecializationRepo, IUnitOfWork unitOfWork) : IConsumer<SpecializationCreated> {
    public async Task Consume(ConsumeContext<SpecializationCreated> context) {
        logger.LogInformation("SpecializationCreated Message received with Id={MessageId}", context.MessageId);

        var Specialization = context.Message.Adapt<Specialization>();
        SpecializationRepo.Create(Specialization);
        await unitOfWork.SaveChangesAsync(context.CancellationToken);
        
        logger.LogInformation("Specialization created with Id={Id} Name={Name}", Specialization.Id, Specialization.Name);
    }
}
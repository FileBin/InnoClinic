using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models;
using InnoClinic.Shared.Misc.Repository;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Service;

using Service = Domain.Models.Service;

public class ServiceUpdatedConsumer(ILogger<ServiceUpdatedConsumer> logger, IRepository<Service> serviceRepo, IUnitOfWork unitOfWork) : IConsumer<ServiceUpdated> {
    public async Task Consume(ConsumeContext<ServiceUpdated> context) {
        logger.LogInformation("ServiceUpdated Message received with Id={MessageId}", context.MessageId);

        var service = await serviceRepo.GetByIdOrThrow(context.Message.Id, context.CancellationToken);
        context.Message.Adapt(service);
        await unitOfWork.SaveChangesAsync();
        
        logger.LogInformation("Service updated with Id={Id} Name={Name}", service.Id, service.Name);
    }
}

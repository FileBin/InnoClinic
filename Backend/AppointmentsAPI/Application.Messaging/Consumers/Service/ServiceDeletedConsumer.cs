using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models;
using InnoClinic.Shared.Misc.Repository;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Service;

using Service = Domain.Models.Service;

public class ServiceDeletedConsumer(ILogger<ServiceDeletedConsumer> logger, IRepository<Service> serviceRepo, IUnitOfWork unitOfWork) : IConsumer<ServiceDeleted> {
    public async Task Consume(ConsumeContext<ServiceDeleted> context) {
        logger.LogInformation("ServiceDeleted Message received with Id={MessageId}", context.MessageId);

        var service = await serviceRepo.GetByIdAsync(context.Message.Id, context.CancellationToken);
        if (service == null) {
            logger.LogWarning("Service with Id={Id} was not found", context.Message.Id);
            return;
        }
        serviceRepo.Delete(service);
        await unitOfWork.SaveChangesAsync();

        logger.LogInformation("Service deleted with Id={Id} Name={Name}", service.Id, service.Name);
    }
}
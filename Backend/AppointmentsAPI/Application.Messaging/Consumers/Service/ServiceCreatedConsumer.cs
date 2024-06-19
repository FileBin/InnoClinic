using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models.Service;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Service;

using Service = Domain.Models.Service;

public class ServiceCreatedConsumer(ILogger<ServiceCreatedConsumer> logger, IRepository<Service> serviceRepo, IUnitOfWork unitOfWork) : IConsumer<ServiceCreated> {
    public async Task Consume(ConsumeContext<ServiceCreated> context) {
        logger.LogInformation("ServiceCreated Message received with Id={MessageId}", context.MessageId);

        var service = context.Message.Adapt<Service>();
        serviceRepo.Create(service);
        await unitOfWork.SaveChangesAsync(context.CancellationToken);
        
        logger.LogInformation("Service created with Id={Id} Name={Name}", service.Id, service.Name);
    }
}
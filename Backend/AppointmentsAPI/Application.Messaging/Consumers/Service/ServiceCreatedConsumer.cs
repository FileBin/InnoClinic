using AppointmentsAPI.Application.Messaging.Abstraction;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models.Service;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Service;

using Service = Domain.Models.Service;

public class ServiceCreatedConsumer(ILogger<ServiceCreatedConsumer> logger, IRepository<Service> serviceRepo, IUnitOfWork unitOfWork)
: CreatedConsumer<ServiceCreated, Service>(logger, serviceRepo, unitOfWork) {}
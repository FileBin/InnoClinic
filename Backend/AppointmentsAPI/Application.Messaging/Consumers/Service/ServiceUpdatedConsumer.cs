using AppointmentsAPI.Application.Messaging.Abstraction;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Service;

using Service = Domain.Models.Service;

public class ServiceUpdatedConsumer(ILogger<ServiceUpdatedConsumer> logger, IRepository<Service> serviceRepo, IUnitOfWork unitOfWork)
: UpdatedConsumer<ServiceUpdated, Service>(logger, serviceRepo, unitOfWork) {}

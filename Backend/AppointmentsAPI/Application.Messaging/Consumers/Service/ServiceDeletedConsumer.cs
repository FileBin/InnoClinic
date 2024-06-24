using AppointmentsAPI.Application.Messaging.Abstraction;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Service;

using Service = Domain.Models.Service;

public class ServiceDeletedConsumer(ILogger<ServiceDeletedConsumer> logger, IRepository<Service> serviceRepo, IUnitOfWork unitOfWork)
: DeletedConsumer<ServiceDeleted, Service>(logger, serviceRepo, unitOfWork) {}
using AppointmentsAPI.Application.Messaging.Abstraction;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Messaging.Contracts.Models.Specialization;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Consumers.Specialization;

using Specialization = Domain.Models.Specialization;

public class SpecializationCreatedConsumer(ILogger<SpecializationCreatedConsumer> logger, IRepository<Specialization> specializationRepo, IUnitOfWork unitOfWork)
: CreatedConsumer<SpecializationCreated, Specialization>(logger, specializationRepo, unitOfWork) {}
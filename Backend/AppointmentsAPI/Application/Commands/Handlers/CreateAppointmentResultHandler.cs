using AppointmentsAPI.Application.Contracts.Handlers;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class CreateAppointmentResultHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAppointmentResultCommand, Guid> {
    public async Task<Guid> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken) {
        var appointmentEntity = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);

        appointmentEntity.AppointmentResult = new AppointmentResult {
            Complaints = request.Complaints ?? "",
            Conclusion = request.Conclusion ?? "",
            Recommendations = request.Recommendations ?? "",
        };

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return appointmentEntity.AppointmentResult.Id;
    }
}

public class UpdateAppointmentResultHandler(IRepository<Appointment> repository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateAppointmentResultCommand> {
    public async Task Handle(UpdateAppointmentResultCommand request, CancellationToken cancellationToken) {
        var appointmentEntity = await repository.GetByIdOrThrow(request.AppointmentId, cancellationToken);

        var appointmentResultEntity = appointmentEntity.AppointmentResult
            ?? throw new BadRequestException("This appointment hasn't result yet!");

        if (!request.UserDescriptor.IsAdmin() && appointmentResultEntity.IsFinished)
            throw new BadRequestException("Result can't be edited after it finished!");

        request.Adapt(appointmentResultEntity);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

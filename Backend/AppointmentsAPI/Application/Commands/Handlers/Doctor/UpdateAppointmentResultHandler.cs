using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using AppointmentsAPI.Application.Queries.Handlers.Patient;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace AppointmentsAPI.Application.Commands.Handlers;

public class UpdateAppointmentResultHandler(IRepository<Appointment> appointmentRepo, IRepository<Doctor> doctorRepo, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAppointmentResultCommand> {
    public async Task Handle(UpdateAppointmentResultCommand request, CancellationToken cancellationToken) {
        var appointmentEntity = await appointmentRepo.GetByIdOrThrow(request.AppointmentId, cancellationToken);

        await appointmentEntity.ValidateAppointmentEditAccessAsync(request, doctorRepo, cancellationToken);

        var appointmentResultEntity = appointmentEntity.AppointmentResult
            ?? throw new BadRequestException("This appointment hasn't result yet!");

        if (!request.UserDescriptor.IsAdmin() && appointmentResultEntity.IsFinished)
            throw new BadRequestException("Result can't be edited after it finished!");

        request.Adapt(appointmentResultEntity);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

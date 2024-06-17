using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Receptionist;

public class RescheduleAppointment(IMediator mediator) : AbstractEndpoint {
    public override string Pattern => "/api/appointment/{appointmentId:guid}";

    public override HttpMethods Method => HttpMethods.Put;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromRoute] Guid appointmentId, [FromBody] TimeSlotRequest request, CancellationToken cancellationToken) => {

        var command = request.Adapt<RescheduleAppointmentCommand>() with {
            AppointmentId = appointmentId,
        };

        await mediator.Send(command, cancellationToken);

        return Results.Ok();
    };
}

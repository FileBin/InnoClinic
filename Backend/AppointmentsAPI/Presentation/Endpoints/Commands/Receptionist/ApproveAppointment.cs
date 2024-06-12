using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Receptionist;

public class ApproveAppointment(IMediator mediator) : AbstractEndpoint {
    public override string Pattern => "/api/appointment/{appointmentId:guid}/approve";

    public override HttpMethods Method => HttpMethods.Patch | HttpMethods.Put;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromRoute] Guid appointmentId, CancellationToken cancellationToken) => {

            var command = new ApproveAppointmentCommand {
                AppointmentId = appointmentId,
            };

            await mediator.Send(command, cancellationToken);

            return Results.Ok();
        };
}
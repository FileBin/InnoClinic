using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Patient;

public class CancelAppointment(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : AbstractEndpoint {
    public override string Pattern => "/api/me/appointment/{appointmentId:guid}";

    public override HttpMethods Method => HttpMethods.Delete;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromRoute] Guid appointmentId,
        ClaimsPrincipal user, CancellationToken cancellationToken) => {

            var command = new CancelAppointmentCommand {
                AppointmentId = appointmentId,
                PatientDescriptor = descriptorFactory.CreateFrom(user),
            };

            await mediator.Send(command, cancellationToken);

            return Results.Ok();
        };
}

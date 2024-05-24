using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Receptionist;

public class CreateAppointment(IMediator mediator) : MultipleEndpoint {
    public override IEnumerable<string> Patterns => ["/api/patient/{patientId:guid}/appointment/", "/api/patient/{patientId:guid}/appointment/create"];

    public override HttpMethods Method => HttpMethods.Post;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromRoute] Guid patientId, [FromBody] CreateAppointmentRequest request, CancellationToken cancellationToken) => {

        var command = request.Adapt<CreateAppointmentCommand>() with {
            PatientId = patientId,
        };

        var response = await mediator.Send(command, cancellationToken);

        return Results.Ok(response);
    };
}

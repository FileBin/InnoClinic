using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;

namespace AppointmentsAPI.Presentation.Endpoints.Queries.Receptionist;

public class GetAppointment(IMediator mediator) : AbstractEndpoint {

    public override string Pattern => "/api/appointment/{appointmentId:guid}";

    public override HttpMethods Method => HttpMethods.Get;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromRoute] Guid appointmentId,
        CancellationToken cancellationToken) => {

        var command = new GetAppointmentQuery {
            AppointmentId = appointmentId,
        };

        var response = await mediator.Send(command, cancellationToken);

        return Results.Ok(response);
    };
}

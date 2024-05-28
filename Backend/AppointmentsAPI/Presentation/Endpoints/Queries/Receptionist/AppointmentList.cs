using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;

namespace AppointmentsAPI.Presentation.Endpoints.Queries.Receptionist;


public class AppointmentList(IMediator mediator) : AbstractEndpoint {

    public override string Pattern => "/api/appointment";

    public override HttpMethods Method => HttpMethods.Get;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromQuery] ViewAppointmentsListQuery request, CancellationToken cancellationToken) => {

        var command = request;

        var response = await mediator.Send(command, cancellationToken);

        return Results.Ok(response);
    };
}

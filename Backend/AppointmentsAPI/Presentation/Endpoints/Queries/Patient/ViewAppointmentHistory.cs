using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;
using InnoClinic.Shared.Domain.Models;

namespace AppointmentsAPI.Presentation.Endpoints.Queries.Patient;

public class ViewAppointmentHistory(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : AbstractEndpoint {

    public override string Pattern => "/api/me/appointment/history";

    public override HttpMethods Method => HttpMethods.Get;

    protected override Delegate EndpointHandler => 
    [Authorize]
    async ([AsParameters] PageDesc pageDesc, 
           ClaimsPrincipal user, CancellationToken cancellationToken) => {

        var command = pageDesc.Adapt<ViewAppointmentHistoryQuery>() with {
            PatientDescriptor = descriptorFactory.CreateFrom(user),
        };

        var response = await mediator.Send(command, cancellationToken);

        return Results.Ok(response);
    };
}

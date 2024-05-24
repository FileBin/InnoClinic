using System.Security.Claims;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;
using InnoClinic.Shared.Misc.Auth;
using InnoClinic.Shared.Misc.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Presentation.Endpoints.Queries.Patient;

using HttpMethods = InnoClinic.Shared.Domain.Models.EndpointHttpMethods;

public class ViewAppointmentResult(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : AbstractEndpoint {

    public override string Pattern => "/api/appointment/{appointmentId:guid}/result";

    public override HttpMethods Method => HttpMethods.Get;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromRoute] Guid appointmentId,
        ClaimsPrincipal user, CancellationToken cancellationToken) => {

        var command = new ViewAppointmentResultQuery {
            AppointmentId = appointmentId,
            PatientDescriptor = descriptorFactory.CreateFrom(user),
        };

        var response = await mediator.Send(command, cancellationToken);

        return Results.Ok(response);
    };
}

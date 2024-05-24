using System.Security.Claims;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using InnoClinic.Shared.Domain.Models;
using InnoClinic.Shared.Misc.Auth;
using InnoClinic.Shared.Misc.Services;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HttpMethods = InnoClinic.Shared.Domain.Models.EndpointHttpMethods;

namespace AppointmentsAPI.Presentation.Endpoints.Queries.Doctor;


public class ViewAppointmentHistory(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : AbstractEndpoint {

    public override string Pattern => "/api/patient/{patientId:guid}/history";

    public override HttpMethods Method => HttpMethods.Get;

    protected override Delegate EndpointHandler => 
    [Authorize]
    async ([FromRoute] Guid patientId, [FromQuery] PageDesc pageDesc, 
           ClaimsPrincipal user, CancellationToken cancellationToken) => {

        var command = pageDesc.Adapt<ViewAppointmentHistoryQuery>() with {
            PatientId = patientId,
            DoctorDescriptor = descriptorFactory.CreateFrom(user),
        };

        var response = await mediator.Send(command, cancellationToken);

        return Results.Ok(response);
    };
}

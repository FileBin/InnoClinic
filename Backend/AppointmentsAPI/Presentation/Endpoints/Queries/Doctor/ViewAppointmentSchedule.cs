using System.Security.Claims;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Common;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using InnoClinic.Shared.Misc.Auth;
using InnoClinic.Shared.Misc.Services;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Presentation.Endpoints.Queries.Doctor;

using HttpMethods = InnoClinic.Shared.Domain.Models.EndpointHttpMethods;

public class ViewAppointmentSchedule(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : AbstractEndpoint {

    public override string Pattern => "/api/me/schedule";

    public override HttpMethods Method => HttpMethods.Get;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromQuery] ViewAppointmentScheduleRequest request,
        ClaimsPrincipal user, CancellationToken cancellationToken) => {

        var command = request.Adapt<ViewAppointmentScheduleQuery>() with {
            DoctorDescriptor = descriptorFactory.CreateFrom(user),
        };

        var response = await mediator.Send(command, cancellationToken);

        return Results.Ok(response);
    };
}

using System.Security.Claims;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using InnoClinic.Shared.Misc.Auth;
using InnoClinic.Shared.Misc.Services;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Doctor;

using HttpMethods = InnoClinic.Shared.Domain.Models.EndpointHttpMethods;

public class UpdateAppointmentResult(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : AbstractEndpoint {

    public override string Pattern => "/api/appointment/{appointmentId:guid}/result";

    public override HttpMethods Method => HttpMethods.Put | HttpMethods.Patch;

    protected override Delegate EndpointHandler => 
    [Authorize]
    async ([FromRoute] Guid appointmentId, [FromBody] UpdateAppointmentResultRequest request, 
           ClaimsPrincipal user, CancellationToken cancellationToken) => {

        var command = request.Adapt<UpdateAppointmentResultCommand>() with {
            AppointmentId = appointmentId,
            DoctorDescriptor = descriptorFactory.CreateFrom(user),
        };

        await mediator.Send(command, cancellationToken);

        return Results.Ok();
    };
}

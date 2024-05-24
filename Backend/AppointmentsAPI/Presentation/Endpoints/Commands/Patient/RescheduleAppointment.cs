using System.Security.Claims;
using AppointmentsAPI.Application.Contracts.Models.Requests;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using InnoClinic.Shared.Misc.Auth;
using InnoClinic.Shared.Misc.Services;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HttpMethods = InnoClinic.Shared.Domain.Models.EndpointHttpMethods;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Patient;

public class RescheduleAppointment(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : AbstractEndpoint {
    public override string Pattern => "/api/appointment/{appointmentId:guid}";

    public override HttpMethods Method => HttpMethods.Put;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromRoute] Guid appointmentId, [FromBody] TimeSlotRequest request,
        ClaimsPrincipal user, CancellationToken cancellationToken) => {

            var command = request.Adapt<RescheduleAppointmentCommand>() with {
                AppointmentId = appointmentId,
                PatientDescriptor = descriptorFactory.CreateFrom(user),
            };

            await mediator.Send(command, cancellationToken);

            return Results.Ok();
        };
}
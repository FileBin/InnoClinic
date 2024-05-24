using System.Security.Claims;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;
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

public class CreateAppointment(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : MultipleEndpoint {
    public override IEnumerable<string> Patterns => [ "/api/appointment/create" , "/api/appointment" ];

    public override HttpMethods Method => HttpMethods.Post;

    protected override Delegate EndpointHandler =>
    [Authorize]
    async ([FromBody] CreateAppointmentRequest request,
        ClaimsPrincipal user, CancellationToken cancellationToken) => {

            var command = request.Adapt<CreateAppointmentCommand>() with {
                PatientDescriptor = descriptorFactory.CreateFrom(user),
            };

            var response = await mediator.Send(command, cancellationToken);

            return Results.Ok(response);
        };
}

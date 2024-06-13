using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Doctor;

public class CreateAppointmentResult(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : AbstractEndpoint {

    public override string Pattern => "/api/appointment/{appointmentId:guid}/result";

    public override HttpMethods Method => HttpMethods.Post;

    protected override Delegate EndpointHandler => 
    [Authorize]
    async ([FromRoute] Guid appointmentId, [FromBody] CreateAppointmentResultRequest request, 
           ClaimsPrincipal user, CancellationToken cancellationToken) => {

        var command = request.Adapt<CreateAppointmentResultCommand>() with {
            AppointmentId = appointmentId,
            DoctorDescriptor = descriptorFactory.CreateFrom(user),
        };

        var response = await mediator.Send(command, cancellationToken);

        return Results.Ok(response);
    };
}

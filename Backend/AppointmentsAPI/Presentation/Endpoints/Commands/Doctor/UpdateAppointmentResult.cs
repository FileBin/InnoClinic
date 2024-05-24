using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Doctor;


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

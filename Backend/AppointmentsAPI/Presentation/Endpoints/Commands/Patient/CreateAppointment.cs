using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Common;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;

namespace AppointmentsAPI.Presentation.Endpoints.Commands.Patient;

public class CreateAppointment(IMediator mediator, ClaimUserDescriptorFactory descriptorFactory) : MultipleEndpoint {
    public override IEnumerable<string> Patterns => [ "/api/me/appointment/create" , "/api/me/appointment" ];

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

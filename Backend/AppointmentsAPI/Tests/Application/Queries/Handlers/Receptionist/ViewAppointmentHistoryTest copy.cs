using AppointmentsAPI.Application.Queries.Handlers.Receptionist;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;
using AppointmentsAPI.Tests.Helpers;
using Mapster;
using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Tests.Application.Queries.Handlers.Receptionist;

[TestFixture]
public class GetAppointmentTest : TestBase {
    GetAppointmentHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(Mocks.AppointmentRepo.Object);
    }

    [Test]
    [CancelAfter(3000)]
    public async Task GetAppointmentNormalTest(CancellationToken cancellationToken) {
        var appointment = await handler.Handle(new GetAppointmentQuery {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
        }, cancellationToken);

        Assert.That(appointment, Is.Not.Null);
        Assert.That(appointment, Is.EqualTo(Objects.Appointment.Adapt<AppointmentResponse>()));
    }
}

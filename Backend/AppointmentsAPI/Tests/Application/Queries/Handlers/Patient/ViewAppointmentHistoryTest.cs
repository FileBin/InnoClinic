using AppointmentsAPI.Application.Queries.Handlers.Patient;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;
using AppointmentsAPI.Tests.Helpers;
using Mapster;
using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Tests.Application.Queries.Handlers.Patient;

[TestFixture]
public class ViewAppointmentHistoryTest : TestBase {
    ViewAppointmentHistoryHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(
            Mocks.AppointmentRepo.Object,
            Mocks.PatientRepo.Object);
    }

    [Test]
    [CancelAfter(3000)]
    public async Task ViewAppointmentHistoryNormalTest(CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.PatientUserUUID,
            UserName = "patient",
        });

        var appointments = await handler.Handle(new ViewAppointmentHistoryQuery {
            PageNumber = 1,
            PageSize = 20,
            PatientDescriptor = descriptor.Object,
        }, cancellationToken);

        Assert.That(appointments, Is.Not.Empty);
        Assert.That(appointments.Single().Single(), Is.EqualTo(Objects.Appointment.Adapt<AppointmentResponse>()));
    }
}

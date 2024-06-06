using AppointmentsAPI.Application.Queries.Handlers.Receptionist;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;
using AppointmentsAPI.Tests.Helpers;
using Mapster;
using AppointmentsAPI.Application.Contracts.Models.Responses;

namespace AppointmentsAPI.Tests.Application.Queries.Handlers.Receptionist;

[TestFixture]
public class ViewAppointmentListTest : TestBase {
    ViewAppointmentListHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(Mocks.AppointmentRepo.Object);
    }

    [Test]
    [TestCase(null, null, null)]
    [TestCase(null, "", null)]
    [TestCase(null, "Alexander", null)]
    [TestCase(null, "Alex", null)]
    [TestCase(null, "Alexander Sm", null)]
    [TestCase(null, "Alexander Smith", null)]
    [TestCase(Config.OfficeUUID, null, null)]
    [CancelAfter(3000)]
    public async Task ViewAppointmentListNormalTest(string? officeId, string? doctorFullName, bool? isApproved, CancellationToken cancellationToken) {
        var appointments = await handler.Handle(new ViewAppointmentsListQuery {
            PageNumber = 1,
            PageSize = 20,
            Date = DateOnly.FromDateTime(DateTime.Today + TimeSpan.FromDays(1)),
            DoctorFullName = doctorFullName,
            IsApproved = isApproved,
            OfficeId = officeId is null ? null : Guid.Parse(officeId),
        }, cancellationToken);

        Assert.That(appointments, Is.Not.Empty);
        Assert.That(appointments.Single(), Is.EqualTo(Objects.Appointment.Adapt<AppointmentResponse>()));
    }
}

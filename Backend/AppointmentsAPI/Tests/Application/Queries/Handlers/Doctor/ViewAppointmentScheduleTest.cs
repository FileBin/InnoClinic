using AppointmentsAPI.Application.Queries.Handlers.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using AppointmentsAPI.Tests.Helpers;

namespace AppointmentsAPI.Tests.Application.Queries.Handlers.Doctor;

[TestFixture]
public class ViewAppointmentScheduleTest : TestBase {
    ViewAppointmentScheduleHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(
            Mocks.AppointmentRepo.Object,
            Mocks.DoctorRepo.Object);
    }

    [Test]
    [CancelAfter(3000)]
    [TestCase(0, 0)]
    [TestCase(-24, 0)]
    [TestCase(6, 0)]
    [TestCase(20, 0)]
    [TestCase(1, 1)]
    [Parallelizable(ParallelScope.Self)]
    public async Task ViewAppointmentHistoryNormalTest(int dayOffset, int expectedAppointmentCount, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.DoctorUserUUID,
            UserName = "doctor",
        });

        var appointments = await handler.Handle(new ViewAppointmentScheduleQuery {
            DoctorDescriptor = descriptor.Object,
            Date = DateOnly.FromDateTime(DateTime.Today + TimeSpan.FromDays(dayOffset)),
        }, cancellationToken);

        Assert.That(appointments.Count(), Is.EqualTo(expectedAppointmentCount));
    }
}
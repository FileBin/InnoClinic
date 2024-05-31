using AppointmentsAPI.Application.Commands.Handlers.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Doctor;

[TestFixture]
public class CreateAppointmentResultTest : TestBase {
    CreateAppointmentResultHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();

        handler = new(
            Mocks.AppointmentRepo.Object,
            Mocks.DoctorRepo.Object,
            Mocks.UnitOfWork.Object);
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestCreateAppointmentResultNormal(CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.DoctorUserUUID,
            UserName = "doctor",
        });

        var result = await handler.Handle(new CreateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
        }, cancellationToken);

        Assert.Multiple(() => {
            Assert.That(Objects.Appointment?.AppointmentResult, Is.Not.Null);
            Assert.That(result, Is.EqualTo(Objects.Appointment!.AppointmentResult!.Id));
        });
    }

    [Test]
    [TestCase("d61562c4-fce7-4cf1-b742-d8bb1ec2d616", Config.AppointmentUUID)]
    [TestCase(Config.DoctorUserUUID, "9caed93b-1145-411c-97f1-472a6f1a160c")]
    [TestCase("bb5f3481-e77e-4dbb-a95a-7a8ba96d73a9", "672f8958-9731-4372-b641-f61835e36cb7")]
    [CancelAfter(5000)]
    public Task TestCreateAppointmentResultThrowsNotFound(string userId, string appointmentId, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = userId,
            UserName = "doctor",
        });

        Assert.ThrowsAsync<NotFoundException>(async () =>
        _ = await handler.Handle(new CreateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(appointmentId),
        }, cancellationToken));

        return Task.CompletedTask;
    }
}
using AppointmentsAPI.Application.Commands.Handlers.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Doctor;

[TestFixture]
public class CreateAppointmentResultTest {
    Helpers.Mocks mocks;

    CreateAppointmentResultHandler handler;

    [SetUp]
    public void SetUp() {
        mocks = new();

        handler = new(
            mocks.MockAppointmentRepo.Object,
            mocks.MockDoctorRepo.Object,
            mocks.MockUnitOfWork.Object);
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestCreateAppointmentResultNormal(CancellationToken cancellationToken) {
        var descriptor = Helpers.Mocks.GenUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.DoctorUserUUID,
            UserName = "doctor",
        });

        var result = await handler.Handle(new CreateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
        }, cancellationToken);

        Assert.Multiple(() => {
            Assert.That(mocks.Appointment.AppointmentResult, Is.Not.Null);
            Assert.That(result, Is.EqualTo(mocks.Appointment.AppointmentResult!.Id));
        });
    }

    [Test]
    [TestCase("d61562c4-fce7-4cf1-b742-d8bb1ec2d616", Config.AppointmentUUID)]
    [TestCase(Config.DoctorUserUUID, "9caed93b-1145-411c-97f1-472a6f1a160c")]
    [TestCase("bb5f3481-e77e-4dbb-a95a-7a8ba96d73a9", "672f8958-9731-4372-b641-f61835e36cb7")]
    [CancelAfter(5000)]
    public void TestCreateAppointmentResultThrowsNotFound(string userId, string appointmentId, CancellationToken cancellationToken) {
        var descriptor = Helpers.Mocks.GenUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.DoctorUserUUID,
            UserName = "doctor",
        });

        Assert.ThrowsAsync<NotFoundException>(async () =>
        _ = await handler.Handle(new CreateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(appointmentId),
        }, cancellationToken));
    }
}
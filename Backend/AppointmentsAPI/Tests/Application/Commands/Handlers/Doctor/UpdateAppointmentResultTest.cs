using AppointmentsAPI.Application.Commands.Handlers.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Doctor;

[TestFixture]
public class UpdateAppointmentResultTest {
    Helpers.Mocks mocks;
    UpdateAppointmentResultHandler handler;

    [SetUp]
    public async Task SetUpAsync() {
        mocks = new();

        CreateAppointmentResultHandler createAppointmentResultHandler = new(
            mocks.MockAppointmentRepo.Object,
            mocks.MockDoctorRepo.Object,
            mocks.MockUnitOfWork.Object);

        var descriptor = Helpers.Mocks.GenUserDescriptor(
            isAdmin: false,
            userId: Config.DoctorUserUUID,
            userName: "doctor");

        _ = await createAppointmentResultHandler.Handle(new CreateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
        }, default);

        handler = new(
            mocks.MockAppointmentRepo.Object,
            mocks.MockDoctorRepo.Object,
            mocks.MockUnitOfWork.Object);
    }

    [Test]
    [TestCase("Complaints", "Conclusion", true)]
    [Parallelizable(ParallelScope.All)]
    [CancelAfter(5000)]
    public async Task TestUpdateAppointmentResultNormal(string complaints, string conclusion, bool isFinished, CancellationToken cancellationToken) {
        var descriptor = Helpers.Mocks.GenUserDescriptor(
            isAdmin: false,
            userId: Config.DoctorUserUUID,
            userName: "doctor");

        await handler.Handle(new UpdateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
            Complaints = complaints,
            Conclusion = conclusion,
            IsFinished = isFinished,
        }, cancellationToken);

        Assert.Multiple(() => {
            var result = mocks.Appointment.AppointmentResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Complaints, Is.EqualTo(complaints));
            Assert.That(result!.Conclusion, Is.EqualTo(conclusion));
            Assert.That(result!.IsFinished, Is.EqualTo(isFinished));
        });
    }

    [Test]
    [TestCase("d61562c4-fce7-4cf1-b742-d8bb1ec2d616", Config.AppointmentUUID)]
    [TestCase(Config.DoctorUserUUID, "9caed93b-1145-411c-97f1-472a6f1a160c")]
    [TestCase("bb5f3481-e77e-4dbb-a95a-7a8ba96d73a9", "672f8958-9731-4372-b641-f61835e36cb7")]
    [Parallelizable(ParallelScope.All)]
    [CancelAfter(5000)]
    public void TestUpdateAppointmentResultThrowsNotFound(string userId, string appointmentId, CancellationToken cancellationToken) {
        var descriptor = new Mock<IUserDescriptor>();
        descriptor.Setup(x => x.IsAdmin()).Returns(false);
        descriptor.Setup(x => x.Id).Returns(userId);
        descriptor.Setup(x => x.Name).Returns("doctor");

        Assert.ThrowsAsync<NotFoundException>(async () =>
        await handler.Handle(new UpdateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(appointmentId),
        }, cancellationToken));
    }
}
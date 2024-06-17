using AppointmentsAPI.Application.Commands.Handlers.Patient;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Domain.Models;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Patient;

[TestFixture]
public class CancelAppointmentTest : TestBase {
    CancelAppointmentHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(
            Mocks.AppointmentRepo.Object,
            Mocks.PatientRepo.Object,
            Mocks.UnitOfWork.Object);
    }

    [Test]
    [CancelAfter(3000)]
    public async Task CancelAppointmentNormalTest(CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.PatientUserUUID,
            UserName = "patient",
        });

        await handler.Handle(new CancelAppointmentCommand {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken);

        Assert.That(Objects.Appointment, Is.Null);

        Objects.Mocks.AppointmentRepo.Verify(x => x.Delete(It.IsAny<Appointment>()), Times.Once());
        Objects.Mocks.UnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Test]
    [TestCase(Config.DoctorUserUUID, Config.AppointmentUUID)]
    [TestCase("e8e3f9f8-4e57-4f5c-9ce4-be2debcbd93c", Config.AppointmentUUID)]
    [TestCase("95289b34-46ef-4dae-b18c-9bc59d84aac5", Config.AppointmentUUID)]
    [TestCase(Config.PatientUserUUID, "793bd052-4a53-4ad3-8a31-931b6f4b1d7d")]
    [TestCase(Config.PatientUserUUID, "75e327e2-2768-435c-9608-dfefc4a467b3")]
    [CancelAfter(3000)]
    [Parallelizable(ParallelScope.Self)]
    public Task CancelAppointmentThrowsNotFoundTest(string userId, string appointmentId, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = userId,
            UserName = "patient",
        });

        Assert.ThrowsAsync<NotFoundException>(async () => 
        await handler.Handle(new CancelAppointmentCommand {
            AppointmentId = Guid.Parse(appointmentId),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken));

        return Task.CompletedTask;
    }

}
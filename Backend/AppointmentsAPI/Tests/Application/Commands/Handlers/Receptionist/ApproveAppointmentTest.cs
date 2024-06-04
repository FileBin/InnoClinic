using System.Globalization;
using AppointmentsAPI.Application.Commands.Handlers.Receptionist;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Domain.Models;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Receptionist;

[TestFixture]
public class ApproveAppointmentTest : TestBase {
    ApproveAppointmentHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(Mocks.AppointmentRepo.Object, Mocks.UnitOfWork.Object);
    }

    [Test]
    [CancelAfter(3000)]
    public async Task ApproveAppointmentNormalTest(CancellationToken cancellationToken) {
        await handler.Handle(new ApproveAppointmentCommand {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
        }, cancellationToken);

        Assert.That(Objects.Appointment?.IsApproved, Is.True);

        Objects.Mocks.UnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }


    [Test]
    [TestCase("e8e3f9f8-4e57-4f5c-9ce4-be2debcbd93c")]
    [TestCase("95289b34-46ef-4dae-b18c-9bc59d84aac5")]
    [CancelAfter(3000)]
    [Parallelizable(ParallelScope.Self)]
    public Task ApproveAppointmentThrowsNotFoundTest(string appointmentUUID, CancellationToken cancellationToken) {
        Assert.ThrowsAsync<NotFoundException>(async () =>
        await handler.Handle(new ApproveAppointmentCommand {
            AppointmentId = Guid.Parse(appointmentUUID),
        }, cancellationToken));

        return Task.CompletedTask;
    }
}
using AppointmentsAPI.Application.Commands.Handlers.Receptionist;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Domain.Models;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Receptionist;

[TestFixture]
public class CancelAppointmentTest : TestBase {
    CancelAppointmentHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(Mocks.AppointmentRepo.Object, Mocks.UnitOfWork.Object);
    }

    [Test]
    [CancelAfter(3000)]
    public async Task CancelAppointmentNormalTest(CancellationToken cancellationToken) {
        await handler.Handle(new CancelAppointmentCommand {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
        }, cancellationToken);

        Assert.That(Objects.Appointment, Is.Null);

        Objects.Mocks.AppointmentRepo.Verify(x => x.Delete(It.IsAny<Appointment>()), Times.Once());
        Objects.Mocks.UnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Test]
    [TestCase("793bd052-4a53-4ad3-8a31-931b6f4b1d7d")]
    [TestCase("75e327e2-2768-435c-9608-dfefc4a467b3")]
    [CancelAfter(3000)]
    [Parallelizable(ParallelScope.Self)]
    public Task CancelAppointmentThrowsNotFoundTest(string appointmentId, CancellationToken cancellationToken) {
        Assert.ThrowsAsync<NotFoundException>(async () =>
        await handler.Handle(new CancelAppointmentCommand {
            AppointmentId = Guid.Parse(appointmentId),
        }, cancellationToken));

        return Task.CompletedTask;
    }

}
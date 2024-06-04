using System.Globalization;
using AppointmentsAPI.Application.Commands.Handlers.Receptionist;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Domain.Models;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Receptionist;

[TestFixture]
public class RescheduleAppointmentTest : TestBase {
    RescheduleAppointmentHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(
            Mocks.AppointmentRepo.Object,
            Mocks.UnitOfWork.Object);
    }

    [Test]
    [CancelAfter(3000)]
    [TestCase("02/08/2024", "12:30:00", "12:40:00")]
    public async Task RescheduleAppointmentNormalTest(string date, string beginTime, string endTime, CancellationToken cancellationToken) {
        var info = CultureInfo.GetCultureInfo("en-US");

        await handler.Handle(new RescheduleAppointmentCommand {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
            Date = DateOnly.Parse(date, info),
            BeginTime = TimeOnly.Parse(beginTime, info),
            EndTime = TimeOnly.Parse(endTime, info),
        }, cancellationToken);

        Assert.That(Objects.Appointment, Is.Not.Null);

        Objects.Mocks.UnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Test]
    [CancelAfter(3000)]
    [TestCase("844f0be6-8893-40ac-8b54-2d7734ca5ebf")]
    public Task RescheduleAppointmentThrowsNotFoundTest(string appointmentUUID, CancellationToken cancellationToken) {
        var timeBegin = DateTime.Now + TimeSpan.FromDays(1);
        var timeEnd = timeBegin + TimeSpan.FromMinutes(10);

        Assert.ThrowsAsync<NotFoundException>(async () =>
        await handler.Handle(new RescheduleAppointmentCommand {
            Date = DateOnly.FromDateTime(timeBegin),
            BeginTime = TimeOnly.FromDateTime(timeBegin),
            EndTime = TimeOnly.FromDateTime(timeEnd),
            AppointmentId = Guid.Parse(appointmentUUID),
        }, cancellationToken));

        return Task.CompletedTask;
    }
}
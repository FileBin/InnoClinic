using System.Globalization;
using AppointmentsAPI.Application.Commands.Handlers.Patient;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Domain.Models;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Patient;

[TestFixture]
public class RescheduleAppointmentTest : TestBase {
    RescheduleAppointmentHandler handler;

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
    [TestCase("02/08/2024", "12:30:00", "12:40:00")]
    public async Task RescheduleAppointmentNormalTest(string date, string beginTime, string endTime, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.PatientUserUUID,
            UserName = "patient",
        });

        var info = CultureInfo.GetCultureInfo("en-US");

        await handler.Handle(new RescheduleAppointmentCommand {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
            Date = DateOnly.Parse(date, info),
            BeginTime = TimeOnly.Parse(beginTime, info),
            EndTime = TimeOnly.Parse(endTime, info),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken);

        Assert.That(Objects.Appointment, Is.Not.Null);

        Objects.Mocks.UnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Test]
    [CancelAfter(3000)]
    [TestCase("02/08/2024", "12:30:00", "12:40:00", "107920b4-16cc-4a10-9886-24aa7511078a")]
    public Task RescheduleAppointmentThrowsNotFoundTest(string date, string beginTime, string endTime, string userGuid, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = userGuid,
            UserName = "patient",
        });

        var info = CultureInfo.GetCultureInfo("en-US");

        Assert.ThrowsAsync<NotFoundException>(async () =>
        await handler.Handle(new RescheduleAppointmentCommand {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
            Date = DateOnly.Parse(date, info),
            BeginTime = TimeOnly.Parse(beginTime, info),
            EndTime = TimeOnly.Parse(endTime, info),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken));

        return Task.CompletedTask;
    }
}
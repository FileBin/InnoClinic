using System.Globalization;
using AppointmentsAPI.Application.Commands.Handlers.Receptionist;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Receptionist;
using AppointmentsAPI.Domain.Models;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Receptionist;

[TestFixture]
public class CreateAppointmentTest : TestBase {
    CreateAppointmentHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(Mocks.AppointmentRepo.Object, Mocks.UnitOfWork.Object);
    }

    [Test]
    [CancelAfter(3000)]
    [TestCase("02/08/2024", "12:30:00", "12:40:00")]
    public async Task ApproveAppointmentNormalTest(string date, string beginTime, string endTime, CancellationToken cancellationToken) {
        var info = CultureInfo.GetCultureInfo("en-US");

        await handler.Handle(new CreateAppointmentCommand {
            Date = DateOnly.Parse(date, info),
            BeginTime = TimeOnly.Parse(beginTime, info),
            EndTime = TimeOnly.Parse(endTime, info),
            DoctorId = Guid.Parse(Config.DoctorEntityUUID),
            ServiceId = Guid.Parse(Config.ServiceUUID),
            PatientId = Guid.Parse(Config.PatientEntityUUID),
        }, cancellationToken);

        Assert.That(Objects.Appointment?.IsApproved, Is.True);

        Objects.Mocks.AppointmentRepo.Verify(x => x.Create(It.IsAny<Appointment>()), Times.Once());
        Objects.Mocks.UnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

}
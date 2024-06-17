using System.Globalization;
using AppointmentsAPI.Application.Commands.Validators.Patient;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Application.Services;
using AppointmentsAPI.Tests.Helpers;
using Microsoft.Extensions.Configuration;

namespace AppointmentsAPI.Tests.Application.Commands.Validators.Patient;

[TestFixture]
public class CreateAppointmentValidatorTest : TestBase {
    CreateAppointmentValidator validator;

    [SetUp]
    public override void SetUp() {
        base.SetUp();

        var emptyConfig = new ConfigurationBuilder().Build();

        validator = new(
            new TimeSlotService(Mocks.AppointmentRepo.Object, emptyConfig),
            Mocks.DoctorRepo.Object,
            Mocks.ServiceRepo.Object);
    }

    [Test]
    [CancelAfter(3000)]
    [TestCase(1, "12:30:00", "12:40:00", Config.DoctorEntityUUID, Config.ServiceUUID, true)]
    [TestCase(1, "12:30:00", "13:40:00", Config.DoctorEntityUUID, Config.ServiceUUID, false)]
    [TestCase(1, "12:30:00", "12:31:00", Config.DoctorEntityUUID, Config.ServiceUUID, false)]
    [TestCase(1, "12:30:00", "12:50:00", Config.DoctorEntityUUID, Config.ServiceUUID, true)]
    [TestCase(5, "12:30:00", "12:40:00", Config.DoctorEntityUUID, Config.ServiceUUID, true)]
    [TestCase(7, "12:30:00", "12:40:00", Config.DoctorEntityUUID, Config.ServiceUUID, true)]
    [TestCase(8, "12:30:00", "12:40:00", Config.DoctorEntityUUID, Config.ServiceUUID, false)]
    [TestCase(1, "13:30:00", "12:40:00", Config.DoctorEntityUUID, Config.ServiceUUID, false)]
    [TestCase(-1, "12:30:00", "12:40:00", Config.DoctorEntityUUID, Config.ServiceUUID, false)]
    [TestCase(-5, "12:30:00", "12:40:00", Config.DoctorEntityUUID, Config.ServiceUUID, false)]
    [TestCase(1, "12:30:00", "12:40:00", "a3e7f832-d97f-4b87-aec9-5479ad14bad1", Config.ServiceUUID, false)]
    [TestCase(1, "12:30:00", "12:40:00", Config.DoctorEntityUUID, "5b9017bb-b94c-4cb7-86b4-e66567802c94", false)]
    public async Task CreateAppointmentNormalTest(int dayOffset, string beginTime, string endTime, string doctorGuid, string ServiceGuid, bool expected, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.PatientUserUUID,
            UserName = "patient",
        });

        var date = DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(dayOffset));

        var info = CultureInfo.GetCultureInfo("en-US");

        var validationResult = await validator.ValidateAsync(new CreateAppointmentCommand {
            Date = date,
            BeginTime = TimeOnly.Parse(beginTime, info),
            EndTime = TimeOnly.Parse(endTime, info),
            DoctorId = Guid.Parse(doctorGuid),
            ServiceId = Guid.Parse(ServiceGuid),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken);

        Assert.That(validationResult.IsValid, Is.EqualTo(expected));
    }
}
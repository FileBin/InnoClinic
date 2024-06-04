using System.Globalization;
using AppointmentsAPI.Application.Commands.Validators.Patient;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Application.Services;
using AppointmentsAPI.Tests.Helpers;
using Microsoft.Extensions.Configuration;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Patient;

[TestFixture]
public class RescheduleAppointmentValidatorTest : TestBase {
    RescheduleAppointmentValidator validator;

    [SetUp]
    public override void SetUp() {
        base.SetUp();

        var emptyConfig = new ConfigurationBuilder().Build();

        validator = new(new TimeSlotService(Mocks.AppointmentRepo.Object, emptyConfig));
    }

    [Test]
    [CancelAfter(3000)]
    [TestCase(1, "12:30:00", "12:40:00", true)]
    [TestCase(1, "12:30:00", "13:40:00", false)]
    [TestCase(1, "12:30:00", "12:31:00", false)]
    [TestCase(1, "12:30:00", "12:50:00", true)]
    [TestCase(5, "12:30:00", "12:40:00", true)]
    [TestCase(7, "12:30:00", "12:40:00", true)]
    [TestCase(8, "12:30:00", "12:40:00", false)]
    [TestCase(1, "13:30:00", "12:40:00", false)]
    [TestCase(-1, "12:30:00", "12:40:00", false)]
    [TestCase(-5, "12:30:00", "12:40:00", false)]
    public async Task CreateAppointmentNormalTest(int dayOffset, string beginTime, string endTime, bool expected, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.PatientUserUUID,
            UserName = "patient",
        });

        var date = DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(dayOffset));

        var info = CultureInfo.GetCultureInfo("en-US");

        var validationResult = await validator.ValidateAsync(new RescheduleAppointmentCommand {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
            Date = date,
            BeginTime = TimeOnly.Parse(beginTime, info),
            EndTime = TimeOnly.Parse(endTime, info),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken);

        Assert.That(validationResult.IsValid, Is.EqualTo(expected));
    }
}
using AppointmentsAPI.Application.Queries.Handlers.Patient;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Patient;
using AppointmentsAPI.Tests.Helpers;
using Mapster;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Queries.Handlers.Patient;

[TestFixture]
public class ViewAppointmentResultTest : TestBase {
    ViewAppointmentResultHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(
            Mocks.AppointmentRepo.Object,
            Mocks.PatientRepo.Object);
    }

    [Test]
    [CancelAfter(3000)]
    public async Task ViewAppointmentResultNormalTest(CancellationToken cancellationToken) {
        Objects.Appointment!.AppointmentResult = new() {
            Id = Guid.NewGuid(),
            Complaints = "Some complaints...",
            Conclusion = "Some conclusion...",
            Recommendations = "Some recommendations...",
            Appointment = Objects.Appointment,
            AppointmentId = Objects.Appointment.Id,
            IsFinished = false,
        };

        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.PatientUserUUID,
            UserName = "patient",
        });

        var appointmentResult = await handler.Handle(new ViewAppointmentResultQuery {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken);

        Assert.That(appointmentResult,
            Is.EqualTo(
                Objects.Appointment!.AppointmentResult.Adapt<AppointmentResultResponse>() with {
                    Date = Objects.Appointment.Date,

                    PatientFullName = $"{Objects.Patient.FirstName} {Objects.Patient.LastName} {Objects.Patient.MiddleName}",
                    PatientDateOfBirth = Objects.Patient.DateOfBirth,

                    DoctorFullName = $"{Objects.Doctor.FirstName} {Objects.Doctor.LastName} {Objects.Doctor.MiddleName}",
                    DoctorSpecializationName = Objects.Service.Specialization.Name,

                    ServiceName = Objects.Service.Name,
                }));
    }

    [Test]
    [CancelAfter(3000)]
    [TestCase(Config.PatientUserUUID, Config.AppointmentUUID)]
    [Parallelizable(ParallelScope.Self)]
    public void ViewAppointmentResultThrowsBadRequestTest(string userUUID, string appointmentUUID, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = userUUID,
            UserName = "patient",
        });

        Assert.ThrowsAsync<BadRequestException>(async () =>
        _ = await handler.Handle(new ViewAppointmentResultQuery {
            AppointmentId = Guid.Parse(appointmentUUID),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken));
    }

    [Test]
    [TestCase("bf2b6852-4a37-4009-ab6a-2d07fcd51002", Config.AppointmentUUID)]
    [TestCase(Config.PatientUserUUID, "a7c9b86f-184c-4b7a-9e0b-c0a0339e64ac")]
    [CancelAfter(3000)]
    [Parallelizable(ParallelScope.Self)]
    public void ViewAppointmentResultThrowsNotFoundInvalidDataTest(string userUUID, string appointmentUUID, CancellationToken cancellationToken) {
        Objects.Appointment!.AppointmentResult = new() {
            Id = Guid.NewGuid(),
            Complaints = "Some complaints...",
            Conclusion = "Some conclusion...",
            Recommendations = "Some recommendations...",
            Appointment = Objects.Appointment,
            AppointmentId = Objects.Appointment.Id,
            IsFinished = false,
        };

        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = userUUID,
            UserName = "patient",
        });

        Assert.ThrowsAsync<NotFoundException>(async () =>
        _ = await handler.Handle(new ViewAppointmentResultQuery {
            AppointmentId = Guid.Parse(appointmentUUID),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken));
    }
}
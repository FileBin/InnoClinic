using AppointmentsAPI.Application.Queries.Handlers.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using AppointmentsAPI.Tests.Helpers;
using Mapster;
using AppointmentsAPI.Application.Contracts.Models.Responses;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Queries.Handlers.Doctor;

[TestFixture]
public class ViewAppointmentResultTest : TestBase {
    ViewAppointmentResultHandler handler;

    [SetUp]
    public override void SetUp() {
        base.SetUp();
        handler = new(
            Mocks.AppointmentRepo.Object);
    }

    [Test]
    [CancelAfter(3000)]
    public async Task ViewAppointmentHistoryNormalTest(CancellationToken cancellationToken) {
        Objects.Appointment!.AppointmentResult = new() {
            Id = Guid.NewGuid(),
            Complaints = "Some complaints...",
            Conclusion = "Some conclusion...",
            Recommendations = "Some recommendations...",
            Appointment = Objects.Appointment,
            AppointmentId = Objects.Appointment.Id,
            IsFinished = false,
        };


        var appointmentResult = await handler.Handle(new ViewAppointmentResultQuery {
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
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
    [TestCase("1bae03c1-d690-4481-9468-fba9eecf679f")]
    public void ViewAppointmentHistoryThrowsNotFoundTest(string appointmentUUID, CancellationToken cancellationToken) {
        Assert.ThrowsAsync<NotFoundException>(async () =>
        _ = await handler.Handle(new ViewAppointmentResultQuery {
            AppointmentId = Guid.Parse(appointmentUUID),
        }, cancellationToken));
    }
}

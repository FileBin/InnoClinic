using System.Linq.Expressions;
using System.Security.Cryptography;
using AppointmentsAPI.Application.Commands.Handlers.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Doctor;

using Doctor = Domain.Models.Doctor;

[TestFixture]
public class CreateAppointmentResultTest {
    private const string DoctorUserUUID = "aa965941-defb-4dc1-8a28-9c281e7d23d7";
    private const string AppointmentUUID = "f7e30427-73df-48bd-8e5a-53396ca2fdcd";

    Mock<IRepository<Appointment>> mockAppointmentRepo;
    Mock<IRepository<Doctor>> mockDoctorRepo;

    Mock<IUnitOfWork> mockUnitOfWork;

    Appointment appointment;
    Doctor doctor;

    CreateAppointmentResultHandler handler;

    [SetUp]
    public void SetUp() {
        doctor = new() {
            Id = Guid.NewGuid(),
            FirstName = "Alexander",
            LastName = "Smith",
            MiddleName = "Andreevich",
            OfficeId = Guid.NewGuid(),
            UserId = Guid.Parse(DoctorUserUUID),
        };

        appointment = new() {
            Id = Guid.Parse(AppointmentUUID),
            DoctorId = doctor.Id,
            PatientId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            BeginTime = TimeOnly.FromDateTime(DateTime.Now),
            Date = DateOnly.FromDateTime(DateTime.Now),
            EndTime = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromMinutes(20)),
            IsApproved = true,
        };

        mockAppointmentRepo = new();
        mockAppointmentRepo
            .Setup(x => x.GetByIdAsync(It.IsIn(appointment.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(appointment);


        mockDoctorRepo = new();
        mockDoctorRepo
            .Setup(x => x.GetByIdAsync(It.IsIn(doctor.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctor);

        var queryableMock = new[] { doctor }.BuildMock();

        mockDoctorRepo
            .Setup(x => x.GetAll())
            .Returns(queryableMock);

        mockUnitOfWork = new();
        mockUnitOfWork
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        handler = new(mockAppointmentRepo.Object, mockDoctorRepo.Object, mockUnitOfWork.Object);
    }

    [Test]
    [TestCase(DoctorUserUUID, AppointmentUUID)]
    [TestCase(DoctorUserUUID, AppointmentUUID)]
    [Parallelizable(ParallelScope.All)]
    [CancelAfter(5000)]
    public async Task TestCreateAppointmentResultNormal(string userId, string appointmentId, CancellationToken cancellationToken) {
        var descriptor = new Mock<IUserDescriptor>();
        descriptor.Setup(x => x.IsAdmin()).Returns(false);
        descriptor.Setup(x => x.Id).Returns(userId);
        descriptor.Setup(x => x.Name).Returns("doctor");

        var result = await handler.Handle(new CreateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(appointmentId),
        }, cancellationToken);

        Assert.Multiple(() => {
            Assert.That(appointment.AppointmentResult, Is.Not.Null);
            Assert.That(result, Is.EqualTo(appointment.AppointmentResult!.Id));
        });
    }

    [Test]
    [TestCase("d61562c4-fce7-4cf1-b742-d8bb1ec2d616", AppointmentUUID)]
    [TestCase(DoctorUserUUID, "9caed93b-1145-411c-97f1-472a6f1a160c")]
    [TestCase("bb5f3481-e77e-4dbb-a95a-7a8ba96d73a9", "672f8958-9731-4372-b641-f61835e36cb7")]
    [Parallelizable(ParallelScope.All)]
    [CancelAfter(5000)]
    public void TestCreateAppointmentResultThrowsNotFound(string userId, string appointmentId, CancellationToken cancellationToken) {
        var descriptor = new Mock<IUserDescriptor>();
        descriptor.Setup(x => x.IsAdmin()).Returns(false);
        descriptor.Setup(x => x.Id).Returns(userId);
        descriptor.Setup(x => x.Name).Returns("doctor");

        Assert.ThrowsAsync<NotFoundException>(async () =>
        _ = await handler.Handle(new CreateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(appointmentId),
        }, cancellationToken));
    }
}
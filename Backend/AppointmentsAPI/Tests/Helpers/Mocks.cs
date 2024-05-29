using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using MockQueryable.Moq;

namespace AppointmentsAPI.Tests.Helpers;

public class Mocks {

    public Mock<IRepository<Appointment>> MockAppointmentRepo { get; private set; }
    public Mock<IRepository<Doctor>> MockDoctorRepo { get; private set; }
    public Mock<IUnitOfWork> MockUnitOfWork { get; private set; }
    public Appointment Appointment { get; private set; }
    public Doctor Doctor { get; private set; }

    public static Mock<IUserDescriptor> GenUserDescriptor(bool isAdmin, string userId, string userName) {
        var descriptor = new Mock<IUserDescriptor>();
        descriptor.Setup(x => x.IsAdmin()).Returns(isAdmin);
        descriptor.Setup(x => x.Id).Returns(userId);
        descriptor.Setup(x => x.Name).Returns(userName);
        return descriptor;
    }

    public Mocks() {
        Doctor = new() {
            Id = Guid.NewGuid(),
            FirstName = "Alexander",
            LastName = "Smith",
            MiddleName = "Andreevich",
            OfficeId = Guid.NewGuid(),
            UserId = Guid.Parse(Config.DoctorUserUUID),
        };

        Appointment = new() {
            Id = Guid.Parse(Config.AppointmentUUID),
            DoctorId = Doctor.Id,
            PatientId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            BeginTime = TimeOnly.FromDateTime(DateTime.Now),
            Date = DateOnly.FromDateTime(DateTime.Now),
            EndTime = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromMinutes(20)),
            IsApproved = true,
        };

        MockAppointmentRepo = new();
        MockAppointmentRepo
            .Setup(x => x.GetByIdAsync(It.IsIn(Appointment.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Appointment);


        MockDoctorRepo = new();
        MockDoctorRepo
            .Setup(x => x.GetByIdAsync(It.IsIn(Doctor.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Doctor);

        var queryableMock = new[] { Doctor }.BuildMock();

        MockDoctorRepo
            .Setup(x => x.GetAll())
            .Returns(queryableMock);

        MockUnitOfWork = new();
        MockUnitOfWork
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);
    }
}
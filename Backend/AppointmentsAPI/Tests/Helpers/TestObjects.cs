using System.Globalization;
using AppointmentsAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using MockQueryable.Moq;

namespace AppointmentsAPI.Tests.Helpers;

public class TestObjects {
    public Appointment? Appointment { get; private set; }
    public Doctor Doctor { get; private set; }
    public Patient Patient { get; private set; }
    public Service Service { get; private set; }
    public Specialization Specialization { get; private set; }

    public TestMocks Mocks { get; private set; }

    public TestObjects() {
        Specialization = new() {
            Name = "Dentology",
            Id = Guid.Parse(Config.SpecializationUUID),
        };

        Service = new() {
            Name = "Dentist",
            Id = Guid.Parse(Config.ServiceUUID),
            SpecializationId = Guid.Parse(Config.SpecializationUUID),
            Specialization = Specialization,
        };

        Specialization.Services = [Service];

        Doctor = new() {
            Id = Guid.Parse(Config.DoctorEntityUUID),
            FirstName = "Alexander",
            LastName = "Smith",
            MiddleName = "Andreevich",
            OfficeId = Guid.Parse(Config.OfficeUUID),
            UserId = Guid.Parse(Config.DoctorUserUUID),
        };

        Patient = new() {
            Id = Guid.Parse(Config.PatientEntityUUID),
            FirstName = "Michael",
            LastName = "Jordan",
            MiddleName = "Frank",
            DateOfBirth = DateOnly.Parse("02/02/1992", new CultureInfo("en-US")),
            UserId = Guid.Parse(Config.PatientUserUUID),
        };

        Appointment = new() {
            Id = Guid.Parse(Config.AppointmentUUID),
            DoctorId = Doctor.Id,
            PatientId = Patient.Id,
            ServiceId = Guid.Parse(Config.ServiceUUID),
            BeginTime = TimeOnly.FromDateTime(DateTime.Now),
            Date = DateOnly.FromDateTime(DateTime.Now),
            EndTime = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromMinutes(20)),
            IsApproved = true,
        };

        Mocks = new(this);
    }

    public static Mock<IUserDescriptor> GenMockUserDescriptor(UserDescriptorParams userDescriptorParams) {
        var descriptor = new Mock<IUserDescriptor>();
        descriptor.Setup(x => x.IsAdmin()).Returns(userDescriptorParams.IsAdmin);
        descriptor.Setup(x => x.Id).Returns(userDescriptorParams.UserId);
        descriptor.Setup(x => x.Name).Returns(userDescriptorParams.UserName);
        return descriptor;
    }

    public class TestMocks {

        public Mock<IRepository<Appointment>> AppointmentRepo { get; private set; }
        public Mock<IRepository<Patient>> PatientRepo { get; private set; }
        public Mock<IRepository<Doctor>> DoctorRepo { get; private set; }
        public Mock<IUnitOfWork> UnitOfWork { get; private set; }

        public TestMocks(TestObjects objects) {


            AppointmentRepo = new();
            AppointmentRepo
                .Setup(x => x.GetByIdAsync(It.IsIn(objects.Appointment!.Id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(objects.Appointment);

            AppointmentRepo
                .Setup(x => x.Delete(It.IsIn(objects.Appointment!)))
                .Callback(() => objects.Appointment = null);

            AppointmentRepo
                .Setup(x => x.Create(It.IsAny<Appointment>()))
                .Callback<Appointment>(a => objects.Appointment = a);


            DoctorRepo = new();
            DoctorRepo
                .Setup(x => x.GetByIdAsync(It.IsIn(objects.Doctor.Id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(objects.Doctor);

            var queryableDoctorMock = new[] { objects.Doctor }.BuildMock();

            DoctorRepo
                .Setup(x => x.GetAll())
                .Returns(queryableDoctorMock);

            PatientRepo = new();
            PatientRepo
                .Setup(x => x.GetByIdAsync(It.IsIn(objects.Patient.Id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(objects.Patient);

            var queryablePatientMock = new[] { objects.Patient }.BuildMock();

            PatientRepo
                .Setup(x => x.GetAll())
                .Returns(queryablePatientMock);

            UnitOfWork = new();
            UnitOfWork
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);
        }
    }
}
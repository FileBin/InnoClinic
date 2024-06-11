using AppointmentsAPI.Application.Commands.Handlers.Patient;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Patient;
using AppointmentsAPI.Domain.Models;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Patient;

[TestFixture]
public class CreateAppointmentTest : TestBase {
    CreateAppointmentHandler handler;

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
    public async Task CreateAppointmentNormalTest(string date, string beginTime, string endTime, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.PatientUserUUID,
            UserName = "patient",
        });

        await handler.Handle(new CreateAppointmentCommand {
            Date = DateOnly.Parse(date, Config.CultureInfo),
            BeginTime = TimeOnly.Parse(beginTime, Config.CultureInfo),
            EndTime = TimeOnly.Parse(endTime, Config.CultureInfo),
            DoctorId = Guid.Parse(Config.DoctorEntityUUID),
            ServiceId = Guid.Parse(Config.ServiceUUID),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken);

        Assert.That(Objects.Appointment, Is.Not.Null);

        Objects.Mocks.AppointmentRepo.Verify(x => x.Create(It.IsAny<Appointment>()), Times.Once());
        Objects.Mocks.UnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }


    [Test]
    [TestCase("e8e3f9f8-4e57-4f5c-9ce4-be2debcbd93c")]
    [TestCase("95289b34-46ef-4dae-b18c-9bc59d84aac5")]
    [CancelAfter(3000)]
    [Parallelizable(ParallelScope.Self)]
    public Task CreateAppointmentThrowsNotFoundTest(string userId, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = userId,
            UserName = "patient",
        });

        var timeBegin = DateTime.Now + TimeSpan.FromDays(1);
        var timeEnd = timeBegin + TimeSpan.FromMinutes(10); 

        Assert.ThrowsAsync<NotFoundException>(async () => 
        await handler.Handle(new CreateAppointmentCommand {
            Date = DateOnly.FromDateTime(timeBegin),
            BeginTime = TimeOnly.FromDateTime(timeBegin),
            EndTime = TimeOnly.FromDateTime(timeEnd),
            DoctorId = Guid.Parse(Config.DoctorEntityUUID),
            ServiceId = Guid.Parse(Config.ServiceUUID),
            PatientDescriptor = descriptor.Object,
        }, cancellationToken));

        return Task.CompletedTask;
    }
}
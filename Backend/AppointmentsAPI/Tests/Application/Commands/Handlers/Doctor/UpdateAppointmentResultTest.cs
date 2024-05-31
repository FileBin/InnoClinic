using AppointmentsAPI.Application.Commands.Handlers.Doctor;
using AppointmentsAPI.Application.Contracts.Models.Requests.Commands.Doctor;
using AppointmentsAPI.Tests.Helpers;
using InnoClinic.Shared.Exceptions.Models;

namespace AppointmentsAPI.Tests.Application.Commands.Handlers.Doctor;

[TestFixture]
public class UpdateAppointmentResultTest : TestBase {
    UpdateAppointmentResultHandler handler;

    [SetUp]
    public async Task SetUpAsync() {
        base.SetUp();

        CreateAppointmentResultHandler createAppointmentResultHandler = new(
            Mocks.AppointmentRepo.Object,
            Mocks.DoctorRepo.Object,
            Mocks.UnitOfWork.Object);

        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.DoctorUserUUID,
            UserName = "doctor",
        });

        _ = await createAppointmentResultHandler.Handle(new CreateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
        }, default);

        handler = new(
            Mocks.AppointmentRepo.Object,
            Mocks.DoctorRepo.Object,
            Mocks.UnitOfWork.Object);
    }

    [Test]
    [TestCase("Complaints", "Conclusion", true)]
    [TestCase(
        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cra",
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec pulvinar nibh. Donec commodo varius nibh. Duis purus diam, pulvinar id enim ac, fringilla dapibus lorem. Morbi hendrerit eget lacus et auctor. Donec facilisis ligula id metus bibendum, ut tempus magna imperdiet. Ut non nunc sed ante auctor tincidunt at vitae tellus. Sed maximus, orci eget dictum sollicitudin, urna justo accumsan tellus, vel posuere justo elit non ipsum.",
        true)]
    [Parallelizable(ParallelScope.Self)]
    [CancelAfter(5000)]
    public async Task TestUpdateAppointmentResultNormal(string complaints, string conclusion, bool isFinished, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = Config.DoctorUserUUID,
            UserName = "doctor",
        });

        await handler.Handle(new UpdateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(Config.AppointmentUUID),
            Complaints = complaints,
            Conclusion = conclusion,
            IsFinished = isFinished,
        }, cancellationToken);

        Assert.Multiple(() => {
            var result = Objects.Appointment?.AppointmentResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Complaints, Is.EqualTo(complaints));
            Assert.That(result!.Conclusion, Is.EqualTo(conclusion));
            Assert.That(result!.IsFinished, Is.EqualTo(isFinished));
        });
    }

    [Test]
    [TestCase("d61562c4-fce7-4cf1-b742-d8bb1ec2d616", Config.AppointmentUUID)]
    [TestCase(Config.DoctorUserUUID, "9caed93b-1145-411c-97f1-472a6f1a160c")]
    [TestCase("bb5f3481-e77e-4dbb-a95a-7a8ba96d73a9", "672f8958-9731-4372-b641-f61835e36cb7")]
    [Parallelizable(ParallelScope.Self)]
    [CancelAfter(5000)]
    public void TestUpdateAppointmentResultThrowsNotFound(string userId, string appointmentId, CancellationToken cancellationToken) {
        var descriptor = TestObjects.GenMockUserDescriptor(new() {
            IsAdmin = false,
            UserId = userId,
            UserName = "doctor",
        });

        Assert.ThrowsAsync<NotFoundException>(async () =>
        await handler.Handle(new UpdateAppointmentResultCommand {
            DoctorDescriptor = descriptor.Object,
            AppointmentId = Guid.Parse(appointmentId),
        }, cancellationToken));
    }
}
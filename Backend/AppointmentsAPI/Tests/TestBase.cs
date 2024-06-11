using AppointmentsAPI.Tests.Helpers;

namespace AppointmentsAPI.Tests;

public class TestBase {
    protected TestObjects Objects { get; private set; } = null!;
    protected TestObjects.TestMocks Mocks { get; private set; } = null!;

    public virtual void SetUp() {
        Objects = new();
        Mocks = Objects.Mocks;
    }
}
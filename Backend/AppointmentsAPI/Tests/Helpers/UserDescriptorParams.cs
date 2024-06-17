namespace AppointmentsAPI.Tests.Helpers;

public record UserDescriptorParams {
    public bool IsAdmin { get; init; }
    public required string UserId { get; init; }
    public required string UserName { get; init; }
}

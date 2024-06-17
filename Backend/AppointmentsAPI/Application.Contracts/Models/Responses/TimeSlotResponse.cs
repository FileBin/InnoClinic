namespace AppointmentsAPI.Application.Contracts.Models.Responses;

public record TimeSlotResponse {
    public required TimeOnly BeginTime { get; init; }
    public required TimeOnly EndTime { get; init; }
}

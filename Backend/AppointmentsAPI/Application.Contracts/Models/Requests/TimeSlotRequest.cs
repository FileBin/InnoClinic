namespace AppointmentsAPI.Application.Contracts.Models.Requests;

public record class TimeSlotRequest {
    public required DateOnly Date { get; init; }

    public required TimeOnly BeginTime { get; init; }
    public required TimeOnly EndTime { get; init; }
}

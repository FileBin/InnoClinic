using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Receptionist;
using FluentValidation;

namespace AppointmentsAPI.Application.Queries.Validators.Receptionist;

public class GetAppointmentValidator : AbstractValidator<GetAppointmentQuery> {
    public GetAppointmentValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
    }
}
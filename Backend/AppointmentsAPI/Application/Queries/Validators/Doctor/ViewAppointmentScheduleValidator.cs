using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using FluentValidation;

namespace AppointmentsAPI.Application.Queries.Validators.Doctor;

public class ViewAppointmentScheduleValidator: AbstractValidator<ViewAppointmentScheduleQuery> {
    public ViewAppointmentScheduleValidator() {
        RuleFor(x => x.Date).NotEmpty();
    }
}
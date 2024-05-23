using AppointmentsAPI.Application.Contracts.Models.Requests.Queries.Doctor;
using FluentValidation;

namespace AppointmentsAPI.Application.Queries.Validators.Doctor;

public class ViewAppointmentResultValidator: AbstractValidator<ViewAppointmentResultQuery> {
    public ViewAppointmentResultValidator() {
        RuleFor(x => x.AppointmentId).NotEmpty();
        //TODO add doctor validation
    }
}
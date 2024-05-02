using FluentValidation;

namespace ServicesAPI.Domain;

internal sealed class ServiceValidator : AbstractValidator<Service> {
    public ServiceValidator() {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.SpecializationId).NotEmpty();

        RuleFor(x => x.Name).NotEmpty().Length(2, 128);
        RuleFor(x => x.Price).NotEmpty().ExclusiveBetween(0, 1_000_000);
    }
}

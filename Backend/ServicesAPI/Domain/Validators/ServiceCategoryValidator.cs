using FluentValidation;

namespace ServicesAPI.Domain;

internal sealed class ServiceCategoryValidator : AbstractValidator<ServiceCategory> {
    public ServiceCategoryValidator() {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.TimeSlotSize).NotEmpty().ExclusiveBetween(0, 1000);
        RuleFor(x => x.Name).NotEmpty().Length(2, 128);
    }
}

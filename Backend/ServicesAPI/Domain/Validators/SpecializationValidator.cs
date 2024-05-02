using FluentValidation;

namespace ServicesAPI.Domain;

internal sealed class SpecializationValidator : AbstractValidator<Specialization> {
    public SpecializationValidator() {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().Length(2, 128);
    }
}

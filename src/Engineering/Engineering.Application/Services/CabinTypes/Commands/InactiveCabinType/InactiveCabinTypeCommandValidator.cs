namespace Engineering.Application.Services.CabinTypes.Commands.InactiveCabinType;

public class InactiveCabinTypeCommandValidator : AbstractValidator<InactiveCabinTypeCommand>
{
    public InactiveCabinTypeCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
namespace Engineering.Application.Services.CabinTypes.Models.InactiveCabinType;

public class InactiveCabinTypeValidator : AbstractValidator<InactiveCabinTypeRequest>
{
    public InactiveCabinTypeValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
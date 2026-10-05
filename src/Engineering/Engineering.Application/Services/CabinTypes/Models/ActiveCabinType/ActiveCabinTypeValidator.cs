namespace Engineering.Application.Services.CabinTypes.Models.ActiveCabinType;

public class ActiveCabinTypeValidator : AbstractValidator<ActiveCabinTypeRequest>
{
    public ActiveCabinTypeValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(CabinTypeErrors.IdIsEmpty);
    }
}
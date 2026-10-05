namespace Engineering.Application.Services.CabinTypes.Commands.ActiveCabinType;

public class ActiveCabinTypeCommandValidator : AbstractValidator<ActiveCabinTypeCommand>
{
    public ActiveCabinTypeCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
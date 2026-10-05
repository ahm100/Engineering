namespace Engineering.Application.Services.CabinTypes.Models.StateChangerCabinTypes;

public class StateChangerCabinTypesValidator : AbstractValidator<StateChangerCabinTypesRequest>
{
    public StateChangerCabinTypesValidator()
    {
        RuleFor(v => v.Ids)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleForEach(v => v.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
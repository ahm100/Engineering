namespace Engineering.Application.Services.CabinTypes.Models.CabinTypeGroupDelete;

public class CabinTypeGroupDeleteValidator : AbstractValidator<CabinTypeGroupDeleteRequest>
{
    public CabinTypeGroupDeleteValidator()
    {
        RuleFor(v => v.Ids)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleForEach(v => v.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
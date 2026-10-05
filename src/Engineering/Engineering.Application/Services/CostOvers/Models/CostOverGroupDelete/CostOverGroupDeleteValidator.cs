namespace Engineering.Application.Services.CostOvers.Models.CostOverGroupDelete;

public class CostOverGroupDeleteValidator : AbstractValidator<CostOverGroupDeleteRequest>
{
    public CostOverGroupDeleteValidator()
    {
        RuleFor(v => v.Ids)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleForEach(v => v.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
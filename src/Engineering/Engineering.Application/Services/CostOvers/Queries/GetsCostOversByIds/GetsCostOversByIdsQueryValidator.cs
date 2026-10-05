namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOversByIds;

public class GetsCostOversByIdsQueryValidator : AbstractValidator<GetsCostOversByIdsQuery>
{
    public GetsCostOversByIdsQueryValidator()
    {
        RuleFor(v => v.Ids)
            .NotNull().WithError(GlobalErrors.IdsIsNull)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty);

        RuleForEach(v => v.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByIds;

public class GetsCostOverByIdsQueryValidator : AbstractValidator<GetsCostOverByIdsQuery>
{
    public GetsCostOverByIdsQueryValidator()
    {
        RuleFor(v => v.Items)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);
    }
}
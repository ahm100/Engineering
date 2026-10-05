namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByNameForResponse;

public class GetCostOverByNameForResponseQueryValidator : AbstractValidator<GetCostOverByNameForResponseQuery>
{
    public GetCostOverByNameForResponseQueryValidator()
    {
        RuleFor(v => v.CostName)
            .NotEmpty().WithError(CostOverErrors.CostOverNameIsEmpty);
    }
}

namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByName;

public class GetCostOverByNameQueryValidator : AbstractValidator<GetCostOverByNameQuery>
{
    public GetCostOverByNameQueryValidator()
    {
        RuleFor(v => v.CostOverName)
            .NotEmpty().WithError(CostOverErrors.CostOverNameIsEmpty);
    }
}
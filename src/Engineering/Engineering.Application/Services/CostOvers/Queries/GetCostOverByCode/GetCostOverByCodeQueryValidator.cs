namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByCode;

public class GetCostOverByCodeQueryValidator : AbstractValidator<GetCostOverByCodeQuery>
{
    public GetCostOverByCodeQueryValidator()
    {
        RuleFor(v => v.CostOverCode)
            .NotEmpty().WithError(CostOverErrors.CostOverCodeIsEmpty);
    }
}
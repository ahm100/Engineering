namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByCodeForResponse;

public class GetCostOverByCodeForResponseQueryValidator : AbstractValidator<GetCostOverByCodeForResponseQuery>
{
    public GetCostOverByCodeForResponseQueryValidator()
    {
        RuleFor(v => v.CostOverCode)
            .NotEmpty().WithError(CostOverErrors.CostOverCodeIsEmpty);
    }
}

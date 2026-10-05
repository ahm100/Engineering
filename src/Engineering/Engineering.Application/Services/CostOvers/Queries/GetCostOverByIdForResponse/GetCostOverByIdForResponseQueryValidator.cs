namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverByIdForResponse;

public class GetCostOverByIdForResponseQueryValidator : AbstractValidator<GetCostOverByIdForResponseQuery>
{
    public GetCostOverByIdForResponseQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}

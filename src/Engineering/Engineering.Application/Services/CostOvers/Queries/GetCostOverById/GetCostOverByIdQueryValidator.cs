namespace Engineering.Application.Services.CostOvers.Queries.GetCostOverById;

public class GetCostOverByIdQueryValidator : AbstractValidator<GetCostOverByIdQuery>
{
    public GetCostOverByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
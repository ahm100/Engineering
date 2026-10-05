namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByName;

public class GetCostCenterTypeByNameQueryValidator : AbstractValidator<GetCostCenterTypeByNameQuery>
{
    public GetCostCenterTypeByNameQueryValidator()
    {
        RuleFor(v => v.CostCenterTypeName)
            .NotEmpty().WithError(CostCenterTypeErrors.CostCenterTypeNameIsEmpty);
    }
}
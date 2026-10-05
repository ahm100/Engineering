namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByNameForResponse;

public class GetCostCenterTypeByNameForResponseQueryValidator : AbstractValidator<GetCostCenterTypeByNameForResponseQuery>
{
    public GetCostCenterTypeByNameForResponseQueryValidator()
    {
        RuleFor(v => v.CostCenterTypeName)
            .NotEmpty().WithError(CostCenterTypeErrors.CostCenterTypeNameIsEmpty);
    }
}

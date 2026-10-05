namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByCode;

public class GetCostCenterTypeByCodeQueryValidator : AbstractValidator<GetCostCenterTypeByCodeQuery>
{
    public GetCostCenterTypeByCodeQueryValidator()
    {
        RuleFor(v => v.CostCenterTypeCode)
            .NotEmpty().WithError(CostCenterTypeErrors.CostCenterTypeCodeIsEmpty);
    }
}
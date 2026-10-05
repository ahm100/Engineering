namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByCodeForResponse;

public class GetCostCenterTypeByCodeForResponseQueryValidator : AbstractValidator<GetCostCenterTypeByCodeForResponseQuery>
{
    public GetCostCenterTypeByCodeForResponseQueryValidator()
    {
        RuleFor(v => v.CostCenterTypeCode)
            .NotEmpty().WithError(CostCenterTypeErrors.CostCenterTypeCodeIsEmpty);
    }
}

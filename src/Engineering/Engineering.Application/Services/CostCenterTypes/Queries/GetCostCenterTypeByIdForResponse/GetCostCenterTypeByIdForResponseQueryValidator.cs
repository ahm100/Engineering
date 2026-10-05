namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByIdForResponse;

public class GetCostCenterTypeByIdForResponseQueryValidator : AbstractValidator<GetCostCenterTypeByIdForResponseQuery>
{
    public GetCostCenterTypeByIdForResponseQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostCenterTypeErrors.CostCenterTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}

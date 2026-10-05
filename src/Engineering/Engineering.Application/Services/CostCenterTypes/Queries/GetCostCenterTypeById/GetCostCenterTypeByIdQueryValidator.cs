namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeById;

public class GetCostCenterTypeByIdQueryValidator : AbstractValidator<GetCostCenterTypeByIdQuery>
{
    public GetCostCenterTypeByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostCenterTypeErrors.CostCenterTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
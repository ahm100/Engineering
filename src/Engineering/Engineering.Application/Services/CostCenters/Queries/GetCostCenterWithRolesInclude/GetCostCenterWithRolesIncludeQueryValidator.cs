namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesInclude;

public class GetCostCenterWithRolesIncludeQueryValidator : AbstractValidator<GetCostCenterWithRolesIncludeQuery>
{
    public GetCostCenterWithRolesIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}

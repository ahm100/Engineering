namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesAndUsersInclude;

public class GetCostCenterWithRolesAndUsersIncludeQueryValidator : AbstractValidator<GetCostCenterWithRolesAndUsersIncludeQuery>
{
    public GetCostCenterWithRolesAndUsersIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}

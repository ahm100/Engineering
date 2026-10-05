namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithUsersInclude;

public class GetCostCenterWithUsersIncludeQueryValidator : AbstractValidator<GetCostCenterWithUsersIncludeQuery>
{
    public GetCostCenterWithUsersIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}

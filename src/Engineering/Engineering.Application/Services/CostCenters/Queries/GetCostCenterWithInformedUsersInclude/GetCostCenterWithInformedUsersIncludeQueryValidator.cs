namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithInformedUsersInclude;

public class GetCostCenterWithInformedUsersIncludeQueryValidator : AbstractValidator<GetCostCenterWithInformedUsersIncludeQuery>
{
    public GetCostCenterWithInformedUsersIncludeQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.IdIsEmpty);
    }
}

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Queries.GetsByAuthorizedRoleId;

public class GetsByCostCenterIdQueryValidator : AbstractValidator<GetsByCostCenterIdQuery>
{
    public GetsByCostCenterIdQueryValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterErrors.IdIsEmpty);
    }
}
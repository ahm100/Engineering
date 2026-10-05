namespace Engineering.Application.Services.CostCenterInformedUsers.Queries.GetsByCostCenterId;

public class GetsByCostCenterIdQueryValidator : AbstractValidator<GetsByCostCenterIdQuery>
{
    public GetsByCostCenterIdQueryValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(CostCenterErrors.IdIsEmpty);
    }
}
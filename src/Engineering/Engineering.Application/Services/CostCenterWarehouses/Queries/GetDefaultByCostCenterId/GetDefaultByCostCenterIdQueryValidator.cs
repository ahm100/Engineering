namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetDefaultByCostCenterId;

public class GetDefaultByCostCenterIdQueryValidator : AbstractValidator<GetDefaultByCostCenterIdQuery>
{
    public GetDefaultByCostCenterIdQueryValidator()
    {
        RuleFor(oo => oo.costCenterId).NotNull();
    }
}

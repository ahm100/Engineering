namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetDefaultByCostCenterIds;

public class GetDefaultByCostCenterIdsQueryValidator : AbstractValidator<GetDefaultByCostCenterIdsQuery>
{
    public GetDefaultByCostCenterIdsQueryValidator()
    {
        RuleFor(oo => oo.CostCenterIds).NotNull();
    }
}

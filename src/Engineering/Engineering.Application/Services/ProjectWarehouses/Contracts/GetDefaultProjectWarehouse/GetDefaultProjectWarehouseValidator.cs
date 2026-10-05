namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetDefaultProjectWarehouse;

public class GetDefaultProjectWarehouseValidator : AbstractValidator<GetDefaultProjectWarehouseRequest>
{
    public GetDefaultProjectWarehouseValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);
    }
}

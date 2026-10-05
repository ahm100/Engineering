namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseInventory;

public class GetProjectWarehouseInventoryValidator : AbstractValidator<GetProjectWarehouseInventoryRequest>
{
    public GetProjectWarehouseInventoryValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.ProductId).IsPositive(GlobalCmts.ProductId);
        RuleFor(oo => oo.PageIndex).PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(oo => oo.PageSize).PageSizeZero(GlobalCmts.PageSize);
    }
}

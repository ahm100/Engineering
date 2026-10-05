namespace Engineering.Application.Services.ProjectWarehouses.Contracts.UpdateProjectWarehouse;

public class UpdateProjectWarehouseValidator : AbstractValidator<UpdateProjectWarehouseRequest>
{
    public UpdateProjectWarehouseValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.WarehouseId).IsPositive(GlobalCmts.WarehouseId);
        RuleFor(oo => oo.IsDefault).IsRequiredBool(ProjectWarehouseCmts.IsDefault);
    }
}

namespace Engineering.Application.Services.ProjectWarehouses.Contracts.CreateProjectWarehouse;

public class CreateProjectWarehouseValidator : AbstractValidator<CreateProjectWarehouseRequest>
{
    public CreateProjectWarehouseValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.WarehouseId).IsPositive(GlobalCmts.WarehouseId);
        RuleFor(oo => oo.IsDefault).IsRequiredBool(ProjectWarehouseCmts.IsDefault);
    }
}

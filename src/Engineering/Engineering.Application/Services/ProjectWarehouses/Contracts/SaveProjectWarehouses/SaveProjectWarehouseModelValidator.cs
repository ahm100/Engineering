namespace Engineering.Application.Services.ProjectWarehouses.Contracts.SaveProjectWarehouses;

public class SaveProjectWarehouseModelValidator : AbstractValidator<SaveProjectWarehouseModel>
{
    public SaveProjectWarehouseModelValidator()
    {
        RuleFor(oo => oo.Id).IsOptionalPositive(GlobalCmts.Id);
        RuleFor(oo => oo.WarehouseId).IsPositive(GlobalCmts.WarehouseId);
        RuleFor(oo => oo.IsDefault).IsRequiredBool(ProjectWarehouseCmts.IsDefault);
    }
}

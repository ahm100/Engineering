namespace Engineering.Application.Services.ProjectWarehouses.Contracts.SaveProjectWarehouses;

public class SaveProjectWarehousesValidator : AbstractValidator<SaveProjectWarehousesRequest>
{
    public SaveProjectWarehousesValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.ProjectWarehouses).NotEmpty();
        RuleFor(oo => oo.ProjectWarehouses).HasNoDuplicates(oo => oo.WarehouseId, GlobalCmts.WarehouseId);
        RuleForEach(oo => oo.ProjectWarehouses).SetValidator(new SaveProjectWarehouseModelValidator());
    }
}

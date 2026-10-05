namespace Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouse;

public class DeleteProjectWarehouseValidator : AbstractValidator<DeleteProjectWarehouseRequest>
{
    public DeleteProjectWarehouseValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}

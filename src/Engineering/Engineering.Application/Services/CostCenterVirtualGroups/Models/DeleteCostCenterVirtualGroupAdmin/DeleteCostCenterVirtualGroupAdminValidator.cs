namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.DeleteCostCenterVirtualGroupAdmin;

public class DeleteCostCenterVirtualGroupAdminValidator : AbstractValidator<DeleteCostCenterVirtualGroupAdminRequest>
{
    public DeleteCostCenterVirtualGroupAdminValidator()
    {
        RuleFor(oo => oo.CostCenterVirtualGroupAdminId)
            .IsPositive(CCenterCmts.CostCenterVirtualGroupAdminId);
    }
}

namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.DeleteCostCenterVirtualGroup;

public class DeleteCostCenterVirtualGroupValidator : AbstractValidator<DeleteCostCenterVirtualGroupRequest>
{
    public DeleteCostCenterVirtualGroupValidator()
    {
        RuleFor(oo => oo.CostCenterVirtualGroupId)
            .IsPositive(CCenterCmts.CostCenterVirtualGroupId);
    }
}

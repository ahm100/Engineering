namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.DeleteCostCenterVirtualGroup;

public class DeleteCostCenterVirtualGroupCommandValidator : AbstractValidator<DeleteCostCenterVirtualGroupCommand>
{
    public DeleteCostCenterVirtualGroupCommandValidator()
    {
        RuleFor(oo => oo.CostCenterVirtualGroupId).NotNull().WithError(CostCenterVirtualGroupErrors.InValidCostCenterVirtualGroup);
    }
}

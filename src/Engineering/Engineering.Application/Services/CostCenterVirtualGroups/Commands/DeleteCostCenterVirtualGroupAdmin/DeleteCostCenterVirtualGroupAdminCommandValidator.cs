namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.DeleteCostCenterVirtualGroupAdmin;

public class DeleteCostCenterVirtualGroupAdminCommandValidator : AbstractValidator<DeleteCostCenterVirtualGroupAdminCommand>
{
    public DeleteCostCenterVirtualGroupAdminCommandValidator()
    {
        RuleFor(oo => oo.CostCenterVirtualGroupAdminId).NotNull().WithError(CostCenterVirtualGroupAdminErrors.InValidCostCenterVirtualGroupAdmin);
    }
}

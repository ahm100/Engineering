namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.UpdateCostCenterVirtualGroupAdmin;

public class UpdateCostCenterVirtualGroupAdminCommandValidator : AbstractValidator<UpdateCostCenterVirtualGroupAdminCommand>
{
    public UpdateCostCenterVirtualGroupAdminCommandValidator()
    {
        RuleFor(oo => oo.ThirdPartyId).NotNull().WithError(CostCenterVirtualGroupAdminErrors.InValidThirdParty);
        RuleFor(oo => oo.UserName).NotEmpty().WithError(CostCenterVirtualGroupAdminErrors.InValidUserName);
        RuleFor(oo => oo.CostCenterVirtualGroupAdminId).NotNull().WithError(CostCenterVirtualGroupAdminErrors.InValidCostCenterVirtualGroupAdmin);
    }
}

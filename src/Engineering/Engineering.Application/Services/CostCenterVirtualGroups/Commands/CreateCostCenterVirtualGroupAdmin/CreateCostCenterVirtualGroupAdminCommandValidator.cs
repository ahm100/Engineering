namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.CreateCostCenterVirtualGroupAdmin;

public class CreateCostCenterVirtualGroupAdminCommandValidator : AbstractValidator<CreateCostCenterVirtualGroupAdminCommand>
{
    public CreateCostCenterVirtualGroupAdminCommandValidator()
    {
        RuleFor(oo => oo.ThirdPartyId).NotNull().WithError(CostCenterVirtualGroupAdminErrors.InValidThirdParty);
        RuleFor(oo => oo.UserName).NotEmpty().WithError(CostCenterVirtualGroupAdminErrors.InValidUserName);
        RuleFor(oo => oo.CostCenterVirtualGroup).NotEmpty().WithError(CostCenterVirtualGroupAdminErrors.InValidCostCenterVirtualGroup);
    }
}

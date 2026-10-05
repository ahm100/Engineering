namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroupAdmin;

public class CreateCostCenterVirtualGroupAdminValidator : AbstractValidator<CreateCostCenterVirtualGroupAdminRequest>
{
    public CreateCostCenterVirtualGroupAdminValidator()
    {
        RuleFor(oo => oo.ThirdPartyId)
            .IsPositive(CCenterCmts.ThirdPartyId);
        RuleFor(oo => oo.UserName)
            .IsRequiredString(CCenterCmts.UserName);
        RuleFor(oo => oo.CostCenterVirtualGroupId)
            .IsPositive(CCenterCmts.CostCenterVirtualGroupId);
    }
}

namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroupAdmin;

public class UpdateCostCenterVirtualGroupAdminValidator : AbstractValidator<UpdateCostCenterVirtualGroupAdminRequest>
{
    public UpdateCostCenterVirtualGroupAdminValidator()
    {
        RuleFor(oo => oo.ThirdPartyId)
            .IsPositive(CCenterCmts.ThirdPartyId);
        RuleFor(oo => oo.UserName)
            .IsRequiredString(CCenterCmts.UserName);
        RuleFor(oo => oo.CostCenterVirtualGroupAdminId)
            .IsPositive(CCenterCmts.CostCenterVirtualGroupAdminId);
    }
}

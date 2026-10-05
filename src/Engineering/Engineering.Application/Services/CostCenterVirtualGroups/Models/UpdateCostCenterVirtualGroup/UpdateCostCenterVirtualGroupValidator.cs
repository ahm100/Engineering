namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroup;

public class UpdateCostCenterVirtualGroupValidator : AbstractValidator<UpdateCostCenterVirtualGroupRequest>
{
    public UpdateCostCenterVirtualGroupValidator()
    {
        RuleFor(oo => oo.Title)
            .IsRequiredString(CCenterCmts.Title);
        RuleFor(oo => oo.Link)
            .IsRequiredString(CCenterCmts.Title);
        RuleFor(oo => oo.Identifier)
            .IsRequiredString(CCenterCmts.Identifier);
        RuleFor(oo => oo.SendToday)
            .IsRequiredBool(CCenterCmts.SendToday);
        RuleFor(oo => oo.SendYesterday)
            .IsRequiredBool(CCenterCmts.SendYesterday);
    }
}

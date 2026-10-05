namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroup;

public class CreateCostCenterVirtualGroupValidator : AbstractValidator<CreateCostCenterVirtualGroupRequest>
{
    public CreateCostCenterVirtualGroupValidator()
    {
        RuleFor(oo => oo.Title)
            .IsRequiredString(CCenterCmts.Title);
        RuleFor(oo => oo.Link)
            .IsRequiredString(CCenterCmts.Link);
        RuleFor(oo => oo.Identifier)
            .IsRequiredString(CCenterCmts.Identifier);
        RuleFor(oo => oo.SendToday)
            .IsRequiredBool(CCenterCmts.SendToday);
        RuleFor(oo => oo.SendYesterday)
            .IsRequiredBool(CCenterCmts.SendYesterday);
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}

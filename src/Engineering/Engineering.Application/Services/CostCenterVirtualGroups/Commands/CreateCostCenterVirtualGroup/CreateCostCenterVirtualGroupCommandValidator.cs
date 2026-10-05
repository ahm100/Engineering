namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.CreateCostCenterVirtualGroup;

public class CreateCostCenterVirtualGroupCommandValidator : AbstractValidator<CreateCostCenterVirtualGroupCommand>
{
    public CreateCostCenterVirtualGroupCommandValidator()
    {
        RuleFor(oo => oo.Title).NotEmpty().WithError(CostCenterVirtualGroupErrors.InValidTitle);
        RuleFor(oo => oo.Link).NotEmpty().WithError(CostCenterVirtualGroupErrors.InValidLink);
        RuleFor(oo => oo.Identifier).NotEmpty().WithError(CostCenterVirtualGroupErrors.InValidIdentifier);
        RuleFor(oo => oo.SendToday).NotEmpty().WithError(CostCenterVirtualGroupErrors.InValidSendToday);
        RuleFor(oo => oo.SendYesterday).NotEmpty().WithError(CostCenterVirtualGroupErrors.InValidSendYesterday);
        RuleFor(oo => oo.CostCenter).NotEmpty().WithError(CostCenterVirtualGroupErrors.InValidCostCenter);
    }
}

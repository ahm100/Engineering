namespace Engineering.Application.RequestGoodsSupplyManagements.Commands.CreateRequestGoodsSupplyManagement;

public class CreateRequestGoodsSupplyManagementCommandValidator : AbstractValidator<CreateRequestGoodsSupplyManagementCommand>
{
    public CreateRequestGoodsSupplyManagementCommandValidator()
    {
        RuleFor(c => c.RequestGoodsSupplyProduct).NotEmpty().WithError(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyDetail);
        RuleFor(c => c.RequestedCount).GreaterThan(0).WithError(RequestGoodsSupplyManagementErrors.InValidRequestedCount);
        RuleFor(c => c.Type).IsInEnum().WithError(RequestGoodsSupplyManagementErrors.InValidType);
    }
}

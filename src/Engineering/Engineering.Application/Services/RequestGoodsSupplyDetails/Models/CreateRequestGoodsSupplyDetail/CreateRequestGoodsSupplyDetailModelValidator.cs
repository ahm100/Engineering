
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;

public class CreateRequestGoodsSupplyDetailModelValidator : AbstractValidator<CreateRequestGoodsSupplyDetailModel>
{
    public CreateRequestGoodsSupplyDetailModelValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyErrors.InValidProjectOperationDetail);
        RuleFor(oo => oo.ProductId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestGoodsSupplyDetailErrors.InValidProduct);
        RuleFor(oo => oo.ProductGroupId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestGoodsSupplyDetailErrors.InValidProductGroupId);
        RuleFor(oo => oo.RequestedCount).GreaterThanOrEqualTo(0).WithError(RequestGoodsSupplyDetailErrors.InValidRequestedCount);
        RuleFor(oo => oo.Importance).IsInEnum().WithError(RequestGoodsSupplyDetailErrors.InValidImportance);
    }
}

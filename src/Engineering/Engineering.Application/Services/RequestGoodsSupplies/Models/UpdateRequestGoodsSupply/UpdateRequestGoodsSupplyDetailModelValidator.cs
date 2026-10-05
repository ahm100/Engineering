
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplies;

public class UpdateRequestGoodsSupplyDetailModelValidator : AbstractValidator<UpdateRequestGoodsSupplyDetailModel>
{
    public UpdateRequestGoodsSupplyDetailModelValidator()
    {
        RuleFor(c => c.ProjectOperationDetailId).NotNull().WithError(RequestGoodsSupplyErrors.InValidProjectOperationDetail);
        RuleFor(c => c.RequestedCount).NotEmpty().WithError(RequestGoodsSupplyDetailErrors.InValidRequestedCount);
        RuleFor(c => c.Importance).IsInEnum().WithError(RequestGoodsSupplyDetailErrors.InValidImportance);
    }
}

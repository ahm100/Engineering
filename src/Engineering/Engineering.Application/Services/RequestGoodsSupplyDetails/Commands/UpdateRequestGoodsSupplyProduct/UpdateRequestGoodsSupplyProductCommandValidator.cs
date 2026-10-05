
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.UpdateRequestGoodsSupplyProduct;

public class UpdateRequestGoodsSupplyProductCommandValidator : AbstractValidator<UpdateRequestGoodsSupplyProductCommand>
{
    public UpdateRequestGoodsSupplyProductCommandValidator()
    {
        RuleFor(c => c.Entity).NotNull().WithError(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailId);
    }
}


namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.UpdateRequestGoodsSupplyDetail;

public class UpdateRequestGoodsSupplyDetailCommandValidator : AbstractValidator<UpdateRequestGoodsSupplyDetailCommand>
{
    public UpdateRequestGoodsSupplyDetailCommandValidator()
    {
        RuleFor(c => c.Entity).NotNull().WithError(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailId);
    }
}

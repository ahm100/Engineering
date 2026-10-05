
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.DeleteRequestGoodsSupplyDetail;

public class DeleteRequestGoodsSupplyDetailCommandValidator : AbstractValidator<DeleteRequestGoodsSupplyDetailCommand>
{
    public DeleteRequestGoodsSupplyDetailCommandValidator()
    {
        RuleFor(oo => oo.Entity).NotNull().WithError(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailId);
    }
}

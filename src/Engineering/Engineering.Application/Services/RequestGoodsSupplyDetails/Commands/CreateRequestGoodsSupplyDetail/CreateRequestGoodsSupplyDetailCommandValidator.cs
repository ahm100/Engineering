
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyDetail;

public class CreateRequestGoodsSupplyDetailCommandValidator : AbstractValidator<CreateRequestGoodsSupplyDetailCommand>
{
    public CreateRequestGoodsSupplyDetailCommandValidator()
    {
        RuleFor(c => c.RequestGoodsSupply).NotEmpty().WithError(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupply);
        RuleFor(c => c.ConsumableVolumeProduct).NotEmpty().WithError(RequestGoodsSupplyDetailErrors.InValidConsumableVolumeProduct);
    }
}

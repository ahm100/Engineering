
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.SetRequestGoodsSupplyProductStatusClose;

public class SetRequestGoodsSupplyProductStatusCloseCommandValidator : AbstractValidator<SetRequestGoodsSupplyProductStatusCloseCommand>
{
    public SetRequestGoodsSupplyProductStatusCloseCommandValidator()
    {
        RuleFor(c => c.Entity).NotNull().WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
    }
}

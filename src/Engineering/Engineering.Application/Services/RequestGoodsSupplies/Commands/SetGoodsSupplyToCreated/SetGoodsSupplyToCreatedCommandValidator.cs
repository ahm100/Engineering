
namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.SetGoodsSupplyToCreated;

public class SetGoodsSupplyToCreatedCommandValidator : AbstractValidator<SetGoodsSupplyToCreatedCommand>
{
    public SetGoodsSupplyToCreatedCommandValidator()
    {
        RuleFor(c => c.GoodsSupply)
            .NotNull()
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
    }
}


namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.SetOperationInfoSeasonToGoodsSupply;

public class SetOperationInfoSeasonToGoodsSupplyCommandValidator : AbstractValidator<SetOperationInfoSeasonToGoodsSupplyCommand>
{
    public SetOperationInfoSeasonToGoodsSupplyCommandValidator()
    {
        RuleFor(oo => oo.OperationInfoSeason)
            .NotEmpty()
            .WithError(RequestGoodsSupplyErrors.InValidOperationInfoSeasonId);
    }
}

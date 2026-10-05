namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRequestGoodsSupply;

public class DeleteRequestGoodsSupplyCommandValidator : AbstractValidator<DeleteRequestGoodsSupplyCommand>
{
    public DeleteRequestGoodsSupplyCommandValidator()
    {
        RuleFor(c => c.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.RequestGoodsSupplyId);
    }
}

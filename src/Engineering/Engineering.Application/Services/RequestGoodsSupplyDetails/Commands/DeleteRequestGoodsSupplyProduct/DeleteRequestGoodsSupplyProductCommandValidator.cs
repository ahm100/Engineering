namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.DeleteRequestGoodsSupplyProduct;

public class DeleteRequestGoodsSupplyProductCommandValidator : AbstractValidator<DeleteRequestGoodsSupplyProductCommand>
{
    public DeleteRequestGoodsSupplyProductCommandValidator()
    {
        RuleFor(c => c.Entity).NotNull().WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
    }
}


namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyProduct;

public class CreateRequestGoodsSupplyProductCommandValidator : AbstractValidator<CreateRequestGoodsSupplyProductCommand>
{
    public CreateRequestGoodsSupplyProductCommandValidator()
    {
        RuleFor(c => c.RequestGoodsSupply)
            .NotEmpty()
            .WithError(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupply);
        RuleFor(c => c.ProductId)
            .IsPositive(GlobalCmts.ProductId);
        RuleFor(c => c.ProductGroupId)
            .IsPositive(GlobalCmts.ProductGroupId);
        RuleFor(c => c.CheckGroup)
            .IsRequiredBool(RGSCmts.CheckGroup);
    }
}

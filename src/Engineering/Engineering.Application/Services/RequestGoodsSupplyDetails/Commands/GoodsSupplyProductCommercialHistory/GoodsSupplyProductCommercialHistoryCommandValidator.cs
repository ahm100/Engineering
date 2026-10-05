
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductCommercialHistory;

public class GoodsSupplyProductCommercialHistoryCommandValidator : AbstractValidator<GoodsSupplyProductCommercialHistoryCommand>
{
    public GoodsSupplyProductCommercialHistoryCommandValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
        RuleFor(c => c.InvoiceId)
            .IsPositive(GlobalCmts.InvoiceId);
        RuleFor(c => c.Status)
            .IsEnum(GlobalCmts.Status);
    }
}

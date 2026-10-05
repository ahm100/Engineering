
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductCommercialStatusChenger;

public class GoodsSupplyProductCommercialStatusChengerCommandValidator : AbstractValidator<GoodsSupplyProductCommercialStatusChengerCommand>
{
    public GoodsSupplyProductCommercialStatusChengerCommandValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
        RuleFor(c => c.InvoiceId)
            .IsPositive(GlobalCmts.InvoiceId);
        RuleFor(c => c.Status)
            .IsEnum(GlobalCmts.Status);
    }
}


namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyDetail;

public class UpdateRequestGoodsSupplyDetailValidator : AbstractValidator<UpdateRequestGoodsSupplyDetailRequest>
{
    public UpdateRequestGoodsSupplyDetailValidator()
    {
        RuleFor(c => c.RequestGoodsSupplyDetailId)
            .IsPositive(GlobalCmts.Id);
        RuleFor(c => c.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(c => c.Importance)
            .IsEnum(RGSCmts.Importance);
    }
}

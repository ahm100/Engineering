namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateTemporaryRequestGoodsSupply;

public class UpdateTemporaryRequestGoodsSupplyValidator : AbstractValidator<UpdateTemporaryRequestGoodsSupplyRequest>
{
    public UpdateTemporaryRequestGoodsSupplyValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.Id);
    }
}
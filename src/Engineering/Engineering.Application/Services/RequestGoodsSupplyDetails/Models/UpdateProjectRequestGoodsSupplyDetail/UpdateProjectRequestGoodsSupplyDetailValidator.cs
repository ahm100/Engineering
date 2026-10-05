namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateProjectRequestGoodsSupplyDetail;

public class UpdateProjectRequestGoodsSupplyDetailValidator : AbstractValidator<UpdateProjectRequestGoodsSupplyDetailRequest>
{
    public UpdateProjectRequestGoodsSupplyDetailValidator()
    {
        RuleFor(c => c.RequestGoodsSupplyDetailId)
            .IsPositive(GlobalCmts.Id);
        RuleFor(c => c.RequestGoodsSupplyDetailId)
            .IsPositive(GlobalCmts.ProjectId);
        RuleFor(c => c.RequestGoodsSupplyDetailId)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(c => c.Importance)
            .IsEnum(RGSCmts.Importance);
    }
}

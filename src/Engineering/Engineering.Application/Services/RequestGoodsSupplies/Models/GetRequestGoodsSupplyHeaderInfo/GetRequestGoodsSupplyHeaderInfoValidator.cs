
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyHeaderInfo;

public class GetRequestGoodsSupplyHeaderInfoValidator : AbstractValidator<GetRequestGoodsSupplyHeaderInfoRequest>
{
    public GetRequestGoodsSupplyHeaderInfoValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
    }
}

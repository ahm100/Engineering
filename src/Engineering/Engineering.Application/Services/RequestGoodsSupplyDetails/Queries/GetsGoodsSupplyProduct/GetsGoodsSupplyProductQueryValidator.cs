namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyProduct;

public class GetsGoodsSupplyProductQueryValidator : AbstractValidator<GetsGoodsSupplyProductQuery>
{
    public GetsGoodsSupplyProductQueryValidator()
    {
        RuleFor(c => c.CompanyId)
            .IsPositive(GlobalCmts.CompanyId);
        RuleFor(c => c.CheckThirdParty)
            .IsRequiredBool(RGSCmts.CheckThirdParty);
    }
}

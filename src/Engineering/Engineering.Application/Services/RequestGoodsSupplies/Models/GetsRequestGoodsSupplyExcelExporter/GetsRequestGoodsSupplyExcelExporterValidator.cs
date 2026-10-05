
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyExcelExporter;

public class GetsRequestGoodsSupplyExcelExporterValidator : AbstractValidator<GetsRequestGoodsSupplyExcelExporterRequest>
{
    public GetsRequestGoodsSupplyExcelExporterValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailsExcelExporter;

public class GetRequestGoodsSupplyDetailsExcelExporterValidator : AbstractValidator<GetRequestGoodsSupplyDetailsExcelExporterRequest>
{
    public GetRequestGoodsSupplyDetailsExcelExporterValidator()
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

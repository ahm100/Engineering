
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice.Exporter;

public class GetSuggestedServicePriceExcelExporterValidator : AbstractValidator<GetSuggestedServicePriceExcelExporterRequest>
{
    public GetSuggestedServicePriceExcelExporterValidator()
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

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader.Exporter;

public class GetsContractorContractExcelExporterValidator : AbstractValidator<GetsContractorContractExcelExporterRequest>
{
    public GetsContractorContractExcelExporterValidator()
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

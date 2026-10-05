
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Exporter;

public class GetsContractorContractReportsExcelExporterValidator : AbstractValidator<GetsContractorContractReportsExcelExporterRequest>
{
    public GetsContractorContractReportsExcelExporterValidator()
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

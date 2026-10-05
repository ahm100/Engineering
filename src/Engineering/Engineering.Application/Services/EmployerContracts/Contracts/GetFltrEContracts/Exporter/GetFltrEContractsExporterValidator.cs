
namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Exporter;

public class GetFltrEContractsExporterValidator : AbstractValidator<GetFltrEContractsExporterRequest>
{
    public GetFltrEContractsExporterValidator()
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
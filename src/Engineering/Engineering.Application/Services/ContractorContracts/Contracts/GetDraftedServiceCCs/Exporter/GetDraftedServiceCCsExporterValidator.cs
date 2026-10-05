
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Exporter;

public class GetDraftedServiceCCsExporterValidator : AbstractValidator<GetDraftedServiceCCsExporterRequest>
{
    public GetDraftedServiceCCsExporterValidator()
    {
        RuleFor(c => c.ContractorId)
           .IsPositive(GlobalCmts.ContractorId);
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

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

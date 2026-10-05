namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Exporter;

public class GetFltrEContractHeadsExporterValidator : AbstractValidator<GetFltrEContractHeadsExporterRequest>
{
    public GetFltrEContractHeadsExporterValidator()
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

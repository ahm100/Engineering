namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;

public class GetsContractorContractServiceReportValidator : AbstractValidator<GetsContractorContractServiceReportRequest>
{
    public GetsContractorContractServiceReportValidator()
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
        RuleFor(x => x.OrderBy)
            .Must(orderBy =>
                RuleExtensions.HasOnlyValidOrderFields<
                    GetsContractorContractServiceReportModel>(orderBy))
            .WithMessage("Invalid OrderBy field.");
    }
}

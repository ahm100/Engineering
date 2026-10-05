namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsIntegratedProjectOperationDetailService;

public class GetsIntegratedProjectOperationDetailServiceValidator : AbstractValidator<GetsIntegratedProjectOperationDetailServiceRequest>
{
    public GetsIntegratedProjectOperationDetailServiceValidator()
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
        RuleFor(x => x.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(x => x.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);

        When(x => x.PageSize > 0, () =>
        {
            RuleFor(x => x.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}

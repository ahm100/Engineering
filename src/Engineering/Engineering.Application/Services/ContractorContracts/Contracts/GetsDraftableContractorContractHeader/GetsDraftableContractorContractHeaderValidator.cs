namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;

public class GetsDraftableContractorContractHeaderValidator : AbstractValidator<GetsDraftableContractorContractHeaderRequest>
{
    public GetsDraftableContractorContractHeaderValidator()
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

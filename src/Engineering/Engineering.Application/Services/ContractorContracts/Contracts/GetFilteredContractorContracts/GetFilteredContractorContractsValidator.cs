namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContracts;

public class GetFilteredContractorContractsValidator : AbstractValidator<GetFilteredContractorContractsRequest>
{
    public GetFilteredContractorContractsValidator()
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

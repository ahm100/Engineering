namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHistory;

public class GetContractorContractHistoryValidator : AbstractValidator<GetContractorContractHistoryRequest>
{
    public GetContractorContractHistoryValidator()
    {
        RuleFor(c => c.ContractorContractId)
            .IsPositive(GlobalCmts.ContractorContractId);
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

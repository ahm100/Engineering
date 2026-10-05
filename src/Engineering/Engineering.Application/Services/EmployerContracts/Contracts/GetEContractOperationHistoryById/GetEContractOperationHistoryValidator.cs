namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;

public class GetEContractOperationHistoryValidator : AbstractValidator<GetEContractOperationHistoryRequest>
{
    public GetEContractOperationHistoryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(EContractCmts.EmployerContract);
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
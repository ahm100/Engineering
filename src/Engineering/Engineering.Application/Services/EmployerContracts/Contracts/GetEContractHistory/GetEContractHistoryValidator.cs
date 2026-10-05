namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;

public class GetEContractHistoryValidator : AbstractValidator<GetEContractHistoryRequest>
{
    public GetEContractHistoryValidator()
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
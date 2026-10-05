namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;

public class GetFltrEContractHeadsValidator : AbstractValidator<GetFltrEContractHeadsRequest>
{
    public GetFltrEContractHeadsValidator()
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

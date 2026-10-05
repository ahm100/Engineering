namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;

public class GetFltrEContractsValidator : AbstractValidator<GetFltrEContractsRequest>
{
    public GetFltrEContractsValidator()
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

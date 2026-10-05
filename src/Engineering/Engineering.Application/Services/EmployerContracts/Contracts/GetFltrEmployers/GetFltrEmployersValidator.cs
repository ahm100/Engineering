namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEmployers;

public class GetFltrEmployersValidator : AbstractValidator<GetFltrEmployersRequest>
{
    public GetFltrEmployersValidator()
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

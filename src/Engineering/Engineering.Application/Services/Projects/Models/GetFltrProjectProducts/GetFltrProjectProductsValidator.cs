namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;

public class GetFltrProjectProductsValidator : AbstractValidator<GetFltrProjectProductsRequest>
{
    public GetFltrProjectProductsValidator()
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

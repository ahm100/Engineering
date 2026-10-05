namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSCreators;

public class GetCSSCreatorsValidator : AbstractValidator<GetCSSCreatorsRequest>
{
    public GetCSSCreatorsValidator()
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

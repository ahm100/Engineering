namespace Engineering.Application.Services.ProjectRisks.Queries.GetFltrProjectRisk;

public class GetFltrProjectRiskQueryValidator : AbstractValidator<GetFltrProjectRiskQuery>
{
    public GetFltrProjectRiskQueryValidator()
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

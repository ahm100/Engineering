namespace Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;

public class GetFltrProjectRiskValidator : AbstractValidator<GetFltrProjectRiskRequest>
{
    public GetFltrProjectRiskValidator()
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
namespace Engineering.Application.Services.WbsTemplates.Queries.GetWbsTemplateForProject;

public class GetWbsTemplateForProjectQueryValidator : AbstractValidator<GetWbsTemplateForProjectQuery>
{
    public GetWbsTemplateForProjectQueryValidator()
    {
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
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
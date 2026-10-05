namespace Engineering.Application.Services.WbsTemplates.Queries.GetFltrWbsTemplate;

public class GetFltrWbsTemplateQueryValidator : AbstractValidator<GetFltrWbsTemplateQuery>
{
    public GetFltrWbsTemplateQueryValidator()
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
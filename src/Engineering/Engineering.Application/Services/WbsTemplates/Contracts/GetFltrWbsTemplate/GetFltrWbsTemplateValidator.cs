namespace Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;

public class GetFltrWbsTemplateValidator : AbstractValidator<GetFltrWbsTemplateRequest>
{
    public GetFltrWbsTemplateValidator()
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
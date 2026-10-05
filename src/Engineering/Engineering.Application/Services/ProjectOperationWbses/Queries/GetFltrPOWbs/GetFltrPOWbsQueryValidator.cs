namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetFltrPOWbs;

public class GetFltrPOWbsQueryValidator : AbstractValidator<GetFltrPOWbsQuery>
{
    public GetFltrPOWbsQueryValidator()
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
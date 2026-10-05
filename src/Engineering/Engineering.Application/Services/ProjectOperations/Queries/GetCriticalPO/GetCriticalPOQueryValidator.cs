namespace Engineering.Application.Services.ProjectOperations.Queries.GetCriticalPO;

public class GetCriticalPOQueryValidator : AbstractValidator<GetCriticalPOQuery>
{
    public GetCriticalPOQueryValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.Id);
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
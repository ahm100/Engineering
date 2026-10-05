namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetDependencyByPOId;

public class GetDependencyByPOIdQueryValidator : AbstractValidator<GetDependencyByPOIdQuery>
{
    public GetDependencyByPOIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
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
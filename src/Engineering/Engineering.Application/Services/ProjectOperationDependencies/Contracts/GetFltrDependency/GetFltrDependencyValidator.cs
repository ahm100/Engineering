namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;

public class GetFltrDependencyValidator : AbstractValidator<GetFltrDependencyRequest>
{
    public GetFltrDependencyValidator()
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
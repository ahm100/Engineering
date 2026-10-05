namespace Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;

public class GetCriticalPOValidator : AbstractValidator<GetCriticalPORequest>
{
    public GetCriticalPOValidator()
    {
        RuleFor(oo => oo.ProjectId)
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
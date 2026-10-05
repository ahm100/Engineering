namespace Engineering.Application.Services.Projects.Models.GetProjectThirdParties;

public class GetFltrProjectThirdPartyValidator : AbstractValidator<GetFltrProjectThirdPartyRequest>
{
    public GetFltrProjectThirdPartyValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(ProjectCmts.ProjectId);
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
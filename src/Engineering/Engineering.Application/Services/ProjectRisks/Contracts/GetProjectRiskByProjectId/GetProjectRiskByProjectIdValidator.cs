namespace Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;

public class GetProjectRiskByProjectIdValidator : AbstractValidator<GetProjectRiskByProjectIdRequest>
{
    public GetProjectRiskByProjectIdValidator()
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

namespace Engineering.Application.Services.EmployerEmployees.Queries.GetECThirdParties;

public class GetECThirdPartiesQueryValidator : AbstractValidator<GetECThirdPartiesQuery>
{
    public GetECThirdPartiesQueryValidator()
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
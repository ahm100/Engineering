namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;

public class GetECThirdPartiesValidator : AbstractValidator<GetECThirdPartiesRequest>
{
    public GetECThirdPartiesValidator()
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
namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrConfigs;

public class GetFltrConfigsValidator : AbstractValidator<GetFltrConfigsRequest>
{
    public GetFltrConfigsValidator()
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
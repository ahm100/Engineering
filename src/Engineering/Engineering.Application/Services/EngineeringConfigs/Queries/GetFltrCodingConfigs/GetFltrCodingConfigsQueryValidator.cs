namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetFltrCodingConfigs;

public class GetFltrCodingConfigsQueryValidator : AbstractValidator<GetFltrCodingConfigsQuery>
{
    public GetFltrCodingConfigsQueryValidator()
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
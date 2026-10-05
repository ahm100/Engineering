namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigByEngConfigId;

public class GetCodingConfigByEngConfigIdValidator : AbstractValidator<GetCodingConfigByEngConfigIdRequest>
{
    public GetCodingConfigByEngConfigIdValidator()
    {
        RuleFor(c => c.ConfigId)
            .IsPositive(EngineeringConfigCmts.EngineeringConfig);
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
namespace Engineering.Application.Services.EngineeringConfigs.Contracts.CreateCodingConfig;

public class CreateCodingConfigValidator : AbstractValidator<CreateCodingConfigRequest>
{
    public CreateCodingConfigValidator()
    {
        RuleFor(oo => oo.Type)
            .IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.Prefix)
            .IsRequiredString(EngineeringConfigCmts.Prefix);
    }
}
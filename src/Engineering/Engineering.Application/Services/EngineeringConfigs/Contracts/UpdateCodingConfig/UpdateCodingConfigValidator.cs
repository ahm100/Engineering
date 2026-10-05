namespace Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateCodingConfig;

public class UpdateCodingConfigValidator : AbstractValidator<UpdateCodingConfigRequest>
{
    public UpdateCodingConfigValidator()
    {
        RuleFor(oo => oo.Type)
            .IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.Prefix)
            .IsRequiredString(EngineeringConfigCmts.Prefix);
    }
}
namespace Engineering.Application.Services.EngineeringConfigs.Commands.UpdateCodingConfig;

public class UpdateCodingConfigCommandValidator : AbstractValidator<UpdateCodingConfigCommand>
{
    public UpdateCodingConfigCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(EngineeringConfigCmts.EngineeringCodingConfig);
        RuleFor(oo => oo.Type)
            .IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.Prefix)
            .IsRequiredString(EngineeringConfigCmts.Prefix);
    }
}
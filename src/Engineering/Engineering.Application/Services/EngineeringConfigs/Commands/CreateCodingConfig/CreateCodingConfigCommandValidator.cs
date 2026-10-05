namespace Engineering.Application.Services.EngineeringConfigs.Commands.CreateCodingConfig;

public class CreateCodingConfigCommandValidator : AbstractValidator<CreateCodingConfigCommand>
{
    public CreateCodingConfigCommandValidator()
    {
        RuleFor(oo => oo.Type)
            .IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.Prefix)
            .IsRequiredString(EngineeringConfigCmts.Prefix);
    }
}
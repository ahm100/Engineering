namespace Engineering.Application.Services.EngineeringConfigs.Contracts.CreateConfig;

public class CreateConfigValidator : AbstractValidator<CreateConfigRequest>
{
    public CreateConfigValidator()
    {
        RuleFor(oo => oo.SendTelegramMessage)
            .IsRequiredBool(EngineeringConfigCmts.SendTelegramMessage);
        RuleFor(oo => oo.ProjectThirdParties)
            .IsRequiredBool(EngineeringConfigCmts.ProjectThirdParties);
    }
}
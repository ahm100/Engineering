namespace Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateConfig;

public class UpdateConfigValidator : AbstractValidator<UpdateConfigRequest>
{
    public UpdateConfigValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(EngineeringConfigCmts.ConfigId);

        RuleFor(oo => oo.SendTelegramMessage)
            .IsRequiredBool(GlobalCmts.IsActive);
    }
}
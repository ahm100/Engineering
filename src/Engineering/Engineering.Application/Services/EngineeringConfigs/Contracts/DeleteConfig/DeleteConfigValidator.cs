namespace Engineering.Application.Services.EngineeringConfigs.Contracts.DeleteConfig;

public class DeleteConfigValidator : AbstractValidator<DeleteConfigRequest>
{
    public DeleteConfigValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(EngineeringConfigCmts.ConfigId);
    }
}
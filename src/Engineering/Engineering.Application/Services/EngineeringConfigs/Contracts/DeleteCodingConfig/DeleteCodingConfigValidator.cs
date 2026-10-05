namespace Engineering.Application.Services.EngineeringConfigs.Contracts.DeleteCodingConfig;

public class DeleteCodingConfigValidator : AbstractValidator<DeleteCodingConfigRequest>
{
    public DeleteCodingConfigValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(EngineeringConfigCmts.ConfigId);
    }
}
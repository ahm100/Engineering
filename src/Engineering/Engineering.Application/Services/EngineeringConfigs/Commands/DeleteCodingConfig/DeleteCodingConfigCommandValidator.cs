namespace Engineering.Application.Services.EngineeringConfigs.Commands.DeleteCodingConfig;

public class DeleteCodingConfigCommandValidator : AbstractValidator<DeleteCodingConfigCommand>
{
    public DeleteCodingConfigCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(EngineeringConfigCmts.EngineeringCodingConfig);
    }
}
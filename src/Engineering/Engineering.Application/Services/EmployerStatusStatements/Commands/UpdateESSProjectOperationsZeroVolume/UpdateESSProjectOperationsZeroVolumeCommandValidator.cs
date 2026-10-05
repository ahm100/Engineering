
namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationsZeroVolume;

public class UpdateESSProjectOperationsZeroVolumeCommandValidator : AbstractValidator<UpdateESSProjectOperationsZeroVolumeCommand>
{
    public UpdateESSProjectOperationsZeroVolumeCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(EmployerStatusStatementErrors.UnValidId);
    }
}

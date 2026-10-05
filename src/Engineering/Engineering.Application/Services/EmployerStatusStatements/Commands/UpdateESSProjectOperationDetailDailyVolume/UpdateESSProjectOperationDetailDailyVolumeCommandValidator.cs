
namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDetailDailyVolume;

public class UpdateESSProjectOperationDetailDailyVolumeCommandValidator : AbstractValidator<UpdateESSProjectOperationDetailDailyVolumeCommand>
{
    public UpdateESSProjectOperationDetailDailyVolumeCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(EmployerStatusStatementErrors.UnValidId);
    }
}

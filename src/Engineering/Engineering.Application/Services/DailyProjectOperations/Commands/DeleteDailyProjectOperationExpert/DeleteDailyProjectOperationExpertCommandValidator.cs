namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationExpert;

public class DeleteDailyProjectOperationExpertCommandValidator : AbstractValidator<DeleteDailyProjectOperationExpertCommand>
{
    public DeleteDailyProjectOperationExpertCommandValidator()
    {
        RuleFor(oo => oo.DeleteDailyProjectOperationExpertId).NotNull().WithError(DailyProjectOperationExpertErrors.DailyProjectOperationExpertWithIdNotFound);
    }
}

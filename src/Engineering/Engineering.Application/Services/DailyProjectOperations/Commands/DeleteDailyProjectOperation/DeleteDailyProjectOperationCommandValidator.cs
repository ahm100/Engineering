namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperation;

public class DeleteDailyProjectOperationCommandValidator : AbstractValidator<DeleteDailyProjectOperationCommand>
{
    public DeleteDailyProjectOperationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(DailyProjectOperationErrors.InValidStartDate);
    }
}

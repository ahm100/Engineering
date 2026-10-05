namespace Engineering.Application.Services.DailyProjectOperations.Models.DeleteDailyProjectOperation;

public class DeleteDailyProjectOperationValidator : AbstractValidator<DeleteDailyProjectOperationRequest>
{
    public DeleteDailyProjectOperationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(DailyProjectOperationErrors.InValidDailyProjectOperationId);
    }
}

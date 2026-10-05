namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationService;

public class DeleteDailyProjectOperationServiceCommandValidator : AbstractValidator<DeleteDailyProjectOperationServiceCommand>
{
    public DeleteDailyProjectOperationServiceCommandValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationServiceId).NotNull().WithError(DailyProjectOperationServiceErrors.DailyProjectOperationServiceWithIdNotFound);
    }
}

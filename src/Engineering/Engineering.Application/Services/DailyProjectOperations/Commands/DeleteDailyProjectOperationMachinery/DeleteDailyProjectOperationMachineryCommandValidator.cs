namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationMachinery;

public class DeleteDailyProjectOperationMachineryCommandValidator : AbstractValidator<DeleteDailyProjectOperationMachineryCommand>
{
    public DeleteDailyProjectOperationMachineryCommandValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationMachineryId).NotNull().WithError(DailyProjectOperationMachineryErrors.DailyProjectOperationMachineryWithIdNotFound);
    }
}

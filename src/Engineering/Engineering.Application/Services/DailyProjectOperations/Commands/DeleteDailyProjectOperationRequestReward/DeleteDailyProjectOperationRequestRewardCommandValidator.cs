namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationRequestReward;

public class DeleteDailyProjectOperationRequestRewardCommandValidator : AbstractValidator<DeleteDailyProjectOperationRequestRewardCommand>
{
    public DeleteDailyProjectOperationRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.DeleteDailyProjectOperationRequestRewardId).NotNull().WithError(DailyProjectOperationExpertErrors.DailyProjectOperationExpertWithIdNotFound);
    }
}

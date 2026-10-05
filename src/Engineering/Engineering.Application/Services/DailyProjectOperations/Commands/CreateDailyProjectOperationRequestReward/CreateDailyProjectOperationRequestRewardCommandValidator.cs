namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationRequestReward;

public class CreateDailyProjectOperationRequestRewardCommandValidator : AbstractValidator<CreateDailyProjectOperationRequestRewardCommand>
{
    public CreateDailyProjectOperationRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.DailyProjectOperation).NotNull().WithError(DailyProjectOperationRequestRewardErrors.InValidDailyProjectOperation);
        RuleFor(oo => oo.RequestRewardId).NotNull().WithError(DailyProjectOperationRequestRewardErrors.InValidRequestRewardId);
    }
}

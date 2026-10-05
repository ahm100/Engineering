namespace Engineering.Application.Services.RequestRewards.Commands.ConfirmRequestReward;

public class ConfirmRequestRewardCommandValidator : AbstractValidator<ConfirmRequestRewardCommand>
{
    public ConfirmRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.ConfirmedPrice).GreaterThan(0).WithError(RequestRewardErrors.InValidConfirmedPrice);
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}

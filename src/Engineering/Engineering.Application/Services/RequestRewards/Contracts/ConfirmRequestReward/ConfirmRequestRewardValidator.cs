namespace Engineering.Application.Services.RequestRewards.Contracts.ConfirmRequestReward;

public class ConfirmRequestRewardValidator : AbstractValidator<ConfirmRequestRewardRequest>
{
    public ConfirmRequestRewardValidator()
    {
        RuleFor(oo => oo.ConfirmedPrice).GreaterThan(0).WithError(RequestRewardErrors.InValidConfirmedPrice);
        RuleFor(oo => oo.ConfirmedPrice).LessThan(9999999999999999).WithError(RequestRewardErrors.ConfirmedPriceCanNotGreater);
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}

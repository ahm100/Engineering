namespace Engineering.Application.Services.RequestRewards.Contracts.CloseRequestReward;

public class CloseRequestRewardValidator : AbstractValidator<CloseRequestRewardRequest>
{
    public CloseRequestRewardValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}


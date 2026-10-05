namespace Engineering.Application.Services.RequestRewards.Commands.CloseRequestReward;

public class CloseRequestRewardCommandValidator : AbstractValidator<CloseRequestRewardCommand>
{
    public CloseRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}


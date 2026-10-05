namespace Engineering.Application.Services.RequestRewards.Commands.RejectRequestReward;

public class RejectRequestRewardCommandValidator : AbstractValidator<RejectRequestRewardCommand>
{
    public RejectRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}


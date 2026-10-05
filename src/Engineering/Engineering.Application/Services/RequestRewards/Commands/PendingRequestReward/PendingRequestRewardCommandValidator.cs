
namespace Engineering.Application.Services.RequestRewards.Commands.PendingRequestReward;

public class PendingRequestRewardCommandValidator : AbstractValidator<PendingRequestRewardCommand>
{
    public PendingRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}


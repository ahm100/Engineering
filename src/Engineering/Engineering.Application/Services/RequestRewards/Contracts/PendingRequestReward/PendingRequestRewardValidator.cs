
namespace Engineering.Application.Services.RequestRewards.Contracts.PendingRequestReward;

public class PendingRequestRewardValidator : AbstractValidator<PendingRequestRewardRequest>
{
    public PendingRequestRewardValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}


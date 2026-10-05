
namespace Engineering.Application.Services.RequestRewards.Contracts.RejectRequestReward;

public class RejectRequestRewardValidator : AbstractValidator<RejectRequestRewardRequest>
{
    public RejectRequestRewardValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}


namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardThirdParty;

public class CreateRequestRewardThirdPartyCommandValidator : AbstractValidator<CreateRequestRewardThirdPartyCommand>
{
    public CreateRequestRewardThirdPartyCommandValidator()
    {
        RuleFor(oo => oo.ThirdPartyId).NotNull().WithError(RequestRewardThirdPartyErrors.InValidThirdParty);
        RuleFor(oo => oo.RequestReward).NotEmpty().WithError(RequestRewardThirdPartyErrors.InValidRequestReward);
    }
}

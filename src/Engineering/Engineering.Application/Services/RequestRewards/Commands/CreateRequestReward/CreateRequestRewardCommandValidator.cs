namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestReward;

public class CreateRequestRewardCommandValidator : AbstractValidator<CreateRequestRewardCommand>
{
    public CreateRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.Type).IsInEnum().WithError(RequestRewardErrors.InValidType);
        RuleFor(oo => oo.RegistrationDate).NotEmpty().WithError(RequestRewardErrors.InValidRegistrationDate);
        RuleFor(oo => oo.Description).NotEmpty().WithError(RequestRewardErrors.InValidDescription);
    }
}

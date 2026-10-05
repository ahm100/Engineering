namespace Engineering.Application.Services.RequestRewards.Commands.UpdateRequestReward;

public class UpdateRequestRewardCommandValidator : AbstractValidator<UpdateRequestRewardCommand>
{
    public UpdateRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestRewardErrors.InValidType);
        RuleFor(oo => oo.Type).IsInEnum().WithError(RequestRewardErrors.InValidType);
        RuleFor(oo => oo.RegistrationDate).NotEmpty().WithError(RequestRewardErrors.InValidRegistrationDate);
        RuleFor(oo => oo.Description).NotEmpty().WithError(RequestRewardErrors.InValidDescription);
    }
}

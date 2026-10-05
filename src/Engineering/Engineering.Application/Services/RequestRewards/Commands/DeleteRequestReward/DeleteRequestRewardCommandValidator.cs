namespace Engineering.Application.Services.RequestRewards.Commands.DeleteRequestReward;

public class DeleteRequestRewardCommandValidator : AbstractValidator<DeleteRequestRewardCommand>
{
    public DeleteRequestRewardCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestRewardErrors.IdIsEmptyForDelete);
    }
}

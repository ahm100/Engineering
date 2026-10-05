namespace Engineering.Application.Services.RequestRewards.Commands.DeleteRequestRewardByIds;

public class DeleteRequestRewardByIdsCommandValidator : AbstractValidator<DeleteRequestRewardByIdsCommand>
{
    public DeleteRequestRewardByIdsCommandValidator()
    {
        RuleForEach(x => x.Ids).IsPositive(RequestRewardErrors.IdIsEmptyForDelete);
    }
}
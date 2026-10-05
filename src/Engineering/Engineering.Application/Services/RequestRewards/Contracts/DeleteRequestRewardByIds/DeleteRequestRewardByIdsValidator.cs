namespace Engineering.Application.Services.RequestRewards.Contracts.DeleteRequestRewardByIds;

public class DeleteRequestRewardByIdsValidator : AbstractValidator<DeleteRequestRewardByIdsRequest>
{
    public DeleteRequestRewardByIdsValidator()
    {
        RuleForEach(x => x.Ids).IsPositive(RequestRewardErrors.IdIsEmptyForDelete);
    }
}

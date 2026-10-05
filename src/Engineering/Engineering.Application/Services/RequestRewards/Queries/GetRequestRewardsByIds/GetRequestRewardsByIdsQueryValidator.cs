namespace Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardsByIds;

public class GetRequestRewardsByIdsQueryValidator : AbstractValidator<GetRequestRewardsByIdsQuery>
{
    public GetRequestRewardsByIdsQueryValidator()
    {
        RuleForEach(c => c.Ids).GreaterThanOrEqualTo(1).WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}

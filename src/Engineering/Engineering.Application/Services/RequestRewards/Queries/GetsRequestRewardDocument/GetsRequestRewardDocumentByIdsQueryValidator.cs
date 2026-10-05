namespace Engineering.Application.Services.RequestRewards.Queries.GetsRequestRewardDocument;

public class GetsRequestRewardDocumentByIdsQueryValidator : AbstractValidator<GetsRequestRewardDocumentByIdsQuery>
{
    public GetsRequestRewardDocumentByIdsQueryValidator()
    {
        RuleForEach(c => c.Ids).NotNull().WithError(RequestRewardDocumentErrors.InValidId);
    }
}
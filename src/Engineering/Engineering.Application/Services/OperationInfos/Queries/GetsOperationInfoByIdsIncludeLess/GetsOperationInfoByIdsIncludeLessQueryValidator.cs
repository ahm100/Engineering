namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsIncludeLess;

public class GetsOperationInfoByIdsIncludeLessQueryValidator : AbstractValidator<GetsOperationInfoByIdsIncludeLessQuery>
{
    public GetsOperationInfoByIdsIncludeLessQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(OperationInfoErrors.IdIsEmpty);
    }
}
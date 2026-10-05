
namespace Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfoBySeasonIds;

internal class GetActiveOperationInfoBySeasonIdsQueryValidator : AbstractValidator<GetActiveOperationInfoBySeasonIdsQuery>
{
    public GetActiveOperationInfoBySeasonIdsQueryValidator()
    {
        RuleFor(c => c.PageIndex)
                .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
                .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
            .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}

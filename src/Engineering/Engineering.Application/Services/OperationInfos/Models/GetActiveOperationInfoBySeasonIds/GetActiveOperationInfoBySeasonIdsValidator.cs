namespace Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfoBySeasonIds;

public class GetActiveOperationInfoBySeasonIdsValidator : AbstractValidator<GetActiveOperationInfoBySeasonIdsRequest>
{
    public GetActiveOperationInfoBySeasonIdsValidator()
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

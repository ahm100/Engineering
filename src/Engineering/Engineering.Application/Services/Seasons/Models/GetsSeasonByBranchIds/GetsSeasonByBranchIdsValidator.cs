namespace Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;

public class GetsSeasonByBranchIdsValidator : AbstractValidator<GetsSeasonByBranchIdsRequest>
{
    public GetsSeasonByBranchIdsValidator()
    {
        RuleForEach(oo => oo.BranchIds)
            .IsPositive(GlobalCmts.BranchId);

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

using Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;

namespace Engineering.Application.Services.Seasons.Queries.GetsSeasonByBranchIdsForResponse;

public class GetsSeasonByBranchIdsForResponseQueryValidator : AbstractValidator<GetsSeasonByBranchIdsForResponseQuery>
{
    public GetsSeasonByBranchIdsForResponseQueryValidator()
    {
        RuleFor(oo => oo.BranchIds).NotEmpty().WithError(SeasonErrors.BranchIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}

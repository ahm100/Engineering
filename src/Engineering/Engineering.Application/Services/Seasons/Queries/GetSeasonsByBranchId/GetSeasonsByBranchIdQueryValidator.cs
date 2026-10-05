namespace Engineering.Application.Services.Seasons.Queries.GetSeasonsByBranchId;

public class GetSeasonsByBranchIdQueryValidator : AbstractValidator<GetSeasonsByBranchIdQuery>
{
    public GetSeasonsByBranchIdQueryValidator()
    {
        RuleFor(oo => oo.BranchId).IsPositive(GlobalCmts.SeasonId);
        RuleFor(oo => oo.CompanyId).IsPositive(GlobalCmts.CompanyId);
    }
}
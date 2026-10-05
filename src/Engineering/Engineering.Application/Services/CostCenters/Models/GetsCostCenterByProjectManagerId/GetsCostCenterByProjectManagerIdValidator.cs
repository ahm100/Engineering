namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByProjectManagerId;

public class GetsCostCenterByProjectManagerIdValidator : AbstractValidator<GetsCostCenterByProjectManagerIdRequest>
{
    public GetsCostCenterByProjectManagerIdValidator()
    {
        RuleFor(oo => oo.ProjectManagerId)
            .IsPositive(GlobalCmts.ProjectManagerId);
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

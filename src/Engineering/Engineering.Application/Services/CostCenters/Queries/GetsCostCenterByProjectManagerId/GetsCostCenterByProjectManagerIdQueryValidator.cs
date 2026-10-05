namespace Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByProjectManagerId;

public class GetsCostCenterByProjectManagerIdQueryValidator : AbstractValidator<GetsCostCenterByProjectManagerIdQuery>
{
    public GetsCostCenterByProjectManagerIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectManagerId).NotNull().WithError(CostCenterErrors.ProjectManagerIdIsEmpty);
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

namespace Engineering.Application.Services.Projects.Queries.GetsProjectByProjectManagerId;

public class GetsProjectByProjectManagerIdQueryValidator : AbstractValidator<GetsProjectByProjectManagerIdQuery>
{
    public GetsProjectByProjectManagerIdQueryValidator()
    {
        RuleFor(oo => oo.CostCenterIds).NotNull().WithError(ProjectErrors.CostCenterIdIsEmpty);
        RuleFor(oo => oo.ProjectManagerId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.ProjectManagerIdIsEmpty);
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
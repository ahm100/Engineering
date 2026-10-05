namespace Engineering.Application.Services.ProjectServices.Queries.GetsServiceInfoByProjectId;

public class GetsServiceInfoByProjectIdQueryValidator : AbstractValidator<GetsServiceInfoByProjectIdQuery>
{
    public GetsServiceInfoByProjectIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .NotNull().WithError(ProjectServiceErrors.ProjectIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.PageIndex)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);

        RuleFor(oo => oo.PageSize)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);

        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex)
                .GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
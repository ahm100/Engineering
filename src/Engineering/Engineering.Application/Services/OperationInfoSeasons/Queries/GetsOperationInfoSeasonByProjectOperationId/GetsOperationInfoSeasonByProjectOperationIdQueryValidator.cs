namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetsOperationInfoSeasonByProjectOperationId;

public class GetsOperationInfoSeasonByProjectOperationIdQueryValidator : AbstractValidator<GetsOperationInfoSeasonByProjectOperationIdQuery>
{
    public GetsOperationInfoSeasonByProjectOperationIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoSeasonErrors.OperationInfoIsEmpty);
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

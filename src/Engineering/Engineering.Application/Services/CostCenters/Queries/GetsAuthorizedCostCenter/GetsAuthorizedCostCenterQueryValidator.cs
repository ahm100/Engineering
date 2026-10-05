namespace Engineering.Application.Services.CostCenters.Queries.GetsAuthorizedCostCenter;

public class GetsAuthorizedCostCenterQueryValidator : AbstractValidator<GetsAuthorizedCostCenterQuery>
{
    public GetsAuthorizedCostCenterQueryValidator()
    {
        RuleFor(oo => oo.UserId)
            .NotNull().GreaterThanOrEqualTo(1).WithError(CostCenterErrors.UserIdIsEmpty);

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

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByNameOrCode;

public class GetsCostOverByNameOrCodeQueryValidator : AbstractValidator<GetsCostOverByNameOrCodeQuery>
{
    public GetsCostOverByNameOrCodeQueryValidator()
    {
        RuleFor(v => v.FilterData)
            .NotEmpty().WithError(CostCenterErrors.FilterDataIsEmpty);

        RuleFor(v => v.PageIndex)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);

        When(w => w.PageSize > 0, () =>
        {
            RuleFor(v => v.PageIndex)
                .GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
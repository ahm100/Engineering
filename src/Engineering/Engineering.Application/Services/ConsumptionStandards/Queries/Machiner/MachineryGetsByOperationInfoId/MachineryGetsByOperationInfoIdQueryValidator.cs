namespace Engineering.Application.Services.ConsumptionStandards.Queries.Machiner.MachineryGetsByOperationInfoId;

public class MachineryGetsByOperationInfoIdQueryValidator : AbstractValidator<MachineryGetsByOperationInfoIdQuery>
{
    public MachineryGetsByOperationInfoIdQueryValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().WithError(MachineryStandardErrors.OprationInfoIdIsEmpty);
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
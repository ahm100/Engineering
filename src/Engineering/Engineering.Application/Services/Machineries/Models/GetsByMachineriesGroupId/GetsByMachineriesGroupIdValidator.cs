namespace Engineering.Application.Services.Machineries.Models.GetsByMachineriesGroupId;

public class GetsByMachineriesGroupIdValidator : AbstractValidator<GetsByMachineriesGroupIdRequest>
{
    public GetsByMachineriesGroupIdValidator()
    {
        RuleFor(oo => oo.MachineriesGroupId).NotNull().GreaterThanOrEqualTo(1).WithError(MachineryErrors.MachineriesGroupIsEmpty);
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

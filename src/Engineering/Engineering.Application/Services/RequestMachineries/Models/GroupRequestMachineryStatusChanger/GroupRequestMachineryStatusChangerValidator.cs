
namespace Engineering.Application.Services.RequestMachineries.Models.GroupRequestMachineryStatusChanger;

public class GroupRequestMachineryStatusChangerValidator : AbstractValidator<GroupRequestMachineryStatusChangerRequest>
{
    public GroupRequestMachineryStatusChangerValidator()
    {
        RuleFor(c => c.Ids)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);

        RuleForEach(c => c.Ids)
            .GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);

        RuleFor(oo => oo.Status)
            .NotNull().WithError(GlobalErrors.StatusIsNull)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}

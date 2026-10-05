
namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GroupProjectOperationTemporaryDailyStatusChanger;

public class GroupProjectOperationTemporaryDailyStatusChangerValidator : AbstractValidator<GroupProjectOperationTemporaryDailyStatusChangerRequest>
{
    public GroupProjectOperationTemporaryDailyStatusChangerValidator()
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
